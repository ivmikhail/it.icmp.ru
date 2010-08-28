using System;
using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;

namespace ITCommunity.DB.Tables {

    public static class Posts {

        public static Post Add(Post post) {
            using (var db = Database.Connect()) {
                db.Posts.InsertOnSubmit(post);
                db.SubmitChanges();

                post.Author.PostsCount++;
                db.SubmitChanges();

                return post;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var post = (
                    from pst in db.Posts
                    where pst.Id == id
                    select pst
                ).SingleOrDefault();

                if (post != null) {
                    if (post.EntityType == Post.EntityTypes.Poll) {
                        Polls.Delete(post.EntityId.Value);
                    }

                    post.Author.PostsCount--;
                    db.SubmitChanges();

                    db.Posts.DeleteOnSubmit(post);
                    db.SubmitChanges();
                }
            }
        }

        public static void Update(Post editedPost) {
            using (var db = Database.Connect()) {
                var post = (
                    from pst in db.Posts
                    where pst.Id == editedPost.Id
                    select pst
                ).Single();

                post.Description = editedPost.Description;
                post.Source = editedPost.Source;
                post.Text = editedPost.Text;
                post.Title = editedPost.Title;
                post.IsAttached = editedPost.IsAttached;
                post.PostsCategories = editedPost.PostsCategories;

                db.SubmitChanges();
            }
        }

        public static void IncViews(Post targetPost) {
            using (var db = Database.Connect()) {
                var post = (
                    from pst in db.Posts
                    where pst.Id == targetPost.Id
                    select pst
                ).Single();

                targetPost.ViewsCount++;
                post.ViewsCount++;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// Получаем пост из базы по id
        /// </summary>
        /// <param name="id">id поста</param>
        /// <returns>Пост</returns>
        public static Post Get(int id) {
            using (var db = Database.Connect()) {
                var post = (
                    from pst in db.Posts
                    where pst.Id == id
                    select pst
                ).SingleOrDefault();

                if (post != null) {
                    post.Comments.Load();
                }

                return post;
            }
        }

        /// <summary>
        /// Забираем посты постранично, с учетом даты и аттачей
        /// </summary>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        /// <param name="totalCount">Общее количество постов</param>
        /// <returns>Список постов для данной страницы</returns>
        public static List<Post> GetPaged(int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var attachedPosts = (
                    from pst in db.Posts
                    where pst.IsAttached
                    orderby pst.CreateDate descending
                    select pst
                ).ToList();

                var disattchedPosts =
                    from pst in db.Posts
                    where pst.IsAttached == false
                    orderby pst.CreateDate descending
                    select pst;

                // учитываем прикрипленные посты
                var disattchedPostsPaged = disattchedPosts.Paged(page, count - attachedPosts.Count, ref totalCount);
                var attachedPostsPages = attachedPosts.Count * totalCount / (count - attachedPosts.Count);
                totalCount = totalCount + attachedPostsPages;

                var posts = attachedPosts;
                posts.AddRange(disattchedPostsPaged);

                return posts;
            }
        }

        /// <summary>
        /// Забираем посты постранично, с учетом даты, аттачей и категории
        /// </summary>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        /// <param name="categoryId">id категории</param>
        /// <param name="totalCount">Общее количество постов категории</param>
        /// <returns>Список постов категории для данной страницы</returns>
        public static List<Post> GetPagedByCategory(int page, int count, int categoryId, ref int totalCount) {
            using (var db = Database.Connect()) {
                var attachedPosts = (
                    from pst in db.Posts
                    from pCat in db.PostsCategories
                    where
                        pCat.CategoryId == categoryId &&
                        pCat.PostId == pst.Id &&
                        pst.IsAttached
                    orderby pst.CreateDate descending
                    select pst
                ).ToList();

                var disattchedPosts =
                    from pst in db.Posts
                    from pCat in db.PostsCategories
                    where
                        pCat.CategoryId == categoryId &&
                        pCat.PostId == pst.Id &&
                        pst.IsAttached == false
                    orderby pst.CreateDate descending
                    select pst;

                var disattchedPostsPaged = disattchedPosts.Paged(page, count - attachedPosts.Count, ref totalCount);

                var posts = attachedPosts;
                posts.AddRange(disattchedPostsPaged);

                return posts;
            }
        }

        public static List<Post> GetPagedFavorite(int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var posts =
                    from pst in db.Posts
                    from fav in db.Favorites
                    where
                        fav.UserId == CurrentUser.User.Id &&
                        fav.PostId == pst.Id
                    orderby pst.CreateDate descending
                    select pst;

                return posts.Paged(page, count, ref totalCount);
            }
        }

        /// <summary>
        /// Забираем посты пользователя постранично, с учетом даты
        /// </summary>
        /// <param name="authorId">id пользователя</param>
        /// <param name="page">Страница которая нам нужна</param>
        /// <param name="count">Кол-во постов на страницу</param>
        /// <param name="postsCount">Общее количество постов</param>
        /// <returns>Список постов для данной страницы</returns>
        public static List<Post> GetPagedByUser(int authorId, int page, int count, ref int totalCount) {
            using (var db = Database.Connect()) {
                var posts =
                    from pst in db.Posts
                    where pst.AuthorId == authorId
                    orderby pst.CreateDate descending
                    select pst;

                return posts.Paged(page, count, ref totalCount);
            }
        }

        public static List<Post> GetPagedPopular(int page, int count, ref int postsCount, int days) {
            using (var db = Database.Connect()) {
                var posts =
                    from pst in db.Posts
                    orderby pst.ViewsCount descending
                    select pst;

                if (days > 0) {
                    var date = DateTime.Now.AddDays(-days);

                    posts =
                        from pst in db.Posts
                        where pst.CreateDate >= date
                        orderby pst.ViewsCount descending
                        select pst;
                }

                return posts.Paged(page, count, ref postsCount);
            }
        }

        public static List<Post> GetLast(int count)
        {
            using (var db = Database.Connect())
            {
                var posts =
                    from pst in db.Posts                    
                    orderby pst.CreateDate descending
                    select pst;

                return posts.Take(count).ToList();
            }
        }


        public static List<Post> GetTopPopular(int count, int days) {
            using (var db = Database.Connect()) {
                var date = DateTime.Now.AddDays(-days);

                var posts =
                    from pst in db.Posts
                    where pst.CreateDate >= date
                    orderby pst.ViewsCount descending
                    select pst;

                return posts.Take(count).ToList();
            }
        }

        public static List<Post> GetTopPopular() {
            int days = Config.GetInt("PopularPostsDays");
            int count = Config.GetInt("PopularPostsCount");

            return AppCache.Get("PopularPosts", () => GetTopPopular(count, days));
        }

        public static List<Post> GetPagedDiscussible(int page, int count, ref int totalCount, int days) {
            using (var db = Database.Connect()) {
                var posts =
                    from pst in db.Posts
                    orderby pst.CommentsCount descending
                    select pst;

                if (days != 0) {
                    var date = DateTime.Now.AddDays(-days);

                    posts =
                        from pst in db.Posts
                        where pst.CreateDate >= date
                        orderby pst.CommentsCount descending
                        select pst;
                }

                return posts.Paged(page, count, ref totalCount);
            }
        }

        public static List<Post> GetTopDiscussible(int count, int days) {
            using (var db = Database.Connect()) {
                var date = DateTime.Now.AddDays(-days);

                var posts =
                    from pst in db.Posts
                    where pst.CreateDate >= date
                    orderby pst.CommentsCount descending
                    select pst;

                return posts.Take(count).ToList();
            }
        }

        public static List<Post> GetTopDiscussible() {
            int days = Config.GetInt("DiscussiblePostsDays");
            int count = Config.GetInt("DiscussiblePostsCount");

            return AppCache.Get("DiscussiblePosts", () => GetTopDiscussible(count, days));
        }

        public static List<Post> GetPagedRated(int page, int count, ref int totalCount, int days) {
            using (var db = Database.Connect()) {
                var posts =
                    from pst in db.Posts
                    from rat in db.Ratings
                    where
                        rat.EntityType == Rating.EntityTypes.Post &
                        rat.EntityId == pst.Id
                    orderby
                        rat.Value descending,
                        pst.CreateDate descending
                    select pst;

                if (days != 0) {
                    var date = DateTime.Now.AddDays(-days);

                    posts =
                        from pst in posts
                        where pst.CreateDate >= date
                        select pst;
                }

                return posts.Paged(page, count, ref totalCount);
            }
        }

        public static List<Post> GetTopRated(int count, int days) {
            using (var db = Database.Connect()) {
                var date = DateTime.Now.AddDays(-days);

                var posts =
                    from pst in db.Posts
                    from rat in db.Ratings
                    where
                        rat.EntityType == Rating.EntityTypes.Post &
                        rat.EntityId == pst.Id &&
                        pst.CreateDate >= date
                    orderby
                        rat.Value descending,
                        pst.CreateDate descending
                    select pst;

                return posts.Take(count).ToList();
            }
        }

        public static List<Post> GetTopRated() {
            int days = Config.GetInt("RatedPostsDays");
            int count = Config.GetInt("RatedPostsCount");

            return AppCache.Get("RatedPosts", () => GetTopRated(count, days));
        }
    }
}
