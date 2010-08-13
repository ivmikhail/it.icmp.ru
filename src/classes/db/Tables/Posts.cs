using System;
using System.Collections.Generic;
using System.Linq;
using ITCommunity.Core;
using ITCommunity.Db;

namespace ITCommunity.Db.Tables {

    public static class Posts {

        /// <summary>
        /// Забираем посты постранично, с учетом даты, аттачей и категории
        /// </summary>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        /// <param name="cat_id">id категории</param>
        public static List<Post> GetPagedByCategory(int page, int count, int categoryId, ref int postsCount) {
            using (var db = Database.Connect()) {
                var postsIds =
                    from postsCategory in db.PostsCategories
                    where postsCategory.CategoryId == categoryId
                    select postsCategory.PostId;

                var posts =
                    from post in db.Posts
                    orderby post.CreateDate descending
                    where postsIds.Contains(post.Id)
                    select post;

                postsCount = posts.Count();

                var result = posts.Skip((page - 1) * count).Take(count);

                return result.ToList();
            }
        }


        public static List<Post> GetPagedFavorite(int page, int count, ref int postsCount) {
            using (var db = Database.Connect()) {
                var posts =
                    from post in db.Posts
                    let favoriteId = (from favorite in db.Favorites where favorite.UserId == CurrentUser.User.Id select favorite.PostId)
                    orderby post.CreateDate descending
                    where favoriteId.Contains(post.Id)
                    select post;

                postsCount = posts.Count();

                var result = posts.Skip((page - 1) * count).Take(count);

                return result.ToList();
            }
        }

        public static bool IsFavorite(int postId) {
            using (var db = Database.Connect()) {
                var favorite =
                    from fav in db.Favorites
                    where
                        fav.PostId == postId &&
                        fav.UserId == CurrentUser.User.Id
                    select fav;

                return favorite.Any();
            }
        }

        /// <summary>
        /// Забираем посты постранично, с учетом даты и аттачей
        /// </summary>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        public static List<Post> GetPaged(int page, int count, ref int postsCount, int days) {
            using (var db = Database.Connect()) {

                var allPosts =
                    from post in db.Posts
                    orderby post.CreateDate descending
                    select post;

                if (days != 0) {
                    var date = DateTime.Now.AddDays(-days);
                    allPosts =
                        from post in db.Posts
                        where post.CreateDate >= date
                        orderby post.CreateDate descending
                        select post;
                }

                postsCount = allPosts.Count();
                var posts = allPosts.Skip((page - 1) * count).Take(count);

                return posts.ToList();
            }
        }


        /// <summary>
        /// Получаем пост из базы по id
        /// </summary>
        /// <param name="id">id</param>
        public static Post Get(int id, bool incViews) {
            using (var db = Database.Connect()) {
                var result = (
                    from post in db.Posts
                    where post.Id == id
                    select post
                ).First();

                if (result != null && incViews) {
                    result.ViewsCount += 1;
                    result.Comments.Load();

                    db.SubmitChanges();
                }

                return result;
            }
        }


        public static Post Get(int id) {
            return Get(id, false);
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<Post> GetPagedPopular(int page, int count, ref int postsCount, int days) {
            using (var db = Database.Connect()) {
                var result = from post in db.Posts
                             orderby post.ViewsCount descending
                             select post;

                if (days != 0) {
                    var date = DateTime.Now.AddDays(-days);

                    result =
                        from post in db.Posts
                        where post.CreateDate >= date
                        orderby post.ViewsCount descending
                        select post;
                }
                postsCount = result.Count();

                var posts = result.Skip((page - 1) * count).Take(count);

                return posts.ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static List<Post> GetPagedDiscussible(int page, int count, ref int postsCount, int days) {
            using (var db = Database.Connect()) {
                var result =
                    from post in db.Posts
                    orderby post.CommentsCount descending
                    select post;

                if (days != 0) {
                    var date = DateTime.Now.AddDays(-days);
                    result =
                        from post in db.Posts
                        where post.CreateDate >= date
                        orderby post.CommentsCount descending
                        select post;
                }
                postsCount = result.Count();

                var posts = result.Skip((page - 1) * count).Take(count);

                return posts.ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static List<Post> GetTopPopular() {
            using (var db = Database.Connect()) {
                int days = Config.GetInt("PopularsDays");
                int count = Config.GetInt("PopularsCount");

                var date = DateTime.Now.AddDays(-days);

                var result =
                    from post in db.Posts
                    where post.CreateDate >= date
                    orderby post.ViewsCount descending
                    select post;

                return result.Take(count).ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public static List<Post> GetTopDiscussible() {
            using (var db = Database.Connect()) {
                int days = Config.GetInt("DiscussionsDays");
                int count = Config.GetInt("DiscussionsCount");

                var date = DateTime.Now.AddDays(-days);

                var result =
                    from post in db.Posts
                    where post.CreateDate >= date
                    orderby post.CommentsCount descending
                    select post;

                return result.Take(count).ToList();
            }
        }


        public static Post Add(Post post) {
            using (var db = Database.Connect()) {
                post.CreateDate = DateTime.Now;

                db.Posts.InsertOnSubmit(post);

                db.SubmitChanges();

                var categories = from cat in post.Categories
                                 select new PostsCategory { PostId = post.Id, CategoryId = cat.Id };

                db.PostsCategories.InsertAllOnSubmit(categories);

                db.SubmitChanges();

                return post;
            }
        }

        public static void Update(Post editedPost) {
            using (var db = Database.Connect()) {
                var post = (
                    from p in db.Posts
                    where p.Id == editedPost.Id
                    select p
                ).Single();

                post.AuthorId = editedPost.AuthorId;
                post.Description = editedPost.Description;
                post.Source = editedPost.Source;
                post.Text = editedPost.Text;
                post.Title = editedPost.Title;

                post.PostsCategories.Clear();

                var categories = from cat in editedPost.Categories
                                 select new PostsCategory { PostId = post.Id, CategoryId = cat.Id };

                db.PostsCategories.InsertAllOnSubmit(categories);

                db.SubmitChanges();
            }
        }
    }

}
