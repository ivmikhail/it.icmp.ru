using System;
using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.Db.Tables {

    public static class Categories {

        public const string CACHE_KEY = "Categories";

        public static List<Category> All {
            get { return AppCache.Get(CACHE_KEY, GetAll); }
        }

        public static Category Add(Category category) {
            using (var db = Database.Connect()) {
                db.Categories.InsertOnSubmit(category);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
                return category;
            }
        }

        public static void Delete(int id) {
//            throw new InvalidOperationException("Нельзя удалять категорию!");
            using (var db = Database.Connect()) {
                var category = (
                    from cat in db.Categories
                    where cat.Id == id
                    select cat
                ).SingleOrDefault();

                db.Categories.DeleteOnSubmit(category);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static void Update(Category editedCategory) {
            using (var db = Database.Connect()) {
                var category = (
                    from cat in db.Categories
                    where cat.Id == editedCategory.Id
                    select cat
                ).SingleOrDefault();

                category.Name = editedCategory.Name;
                category.Sort = editedCategory.Sort;

                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static List<Category> GetAll() {
            using (var db = Database.Connect()) {
                var categories =
                    from cat in db.Categories
                    orderby cat.Sort ascending
                    select cat;

                return categories.ToList();
            }
        }

        public static Category Get(int id) {
            var category = (
                from cat in All
                where cat.Id == id
                select cat
            ).SingleOrDefault();

            return category;
        }

        public static int GetPostsCount(int id) {
            using (var db = Database.Connect()) {
                var categoryPosts =
                    from pCat in db.PostsCategories
                    where pCat.CategoryId == id
                    select pCat;

                return categoryPosts.Count();
            }
        }
        
        public static List<Category> GetByPost(int postId) {
            using (var db = Database.Connect()) {
                var categories =
                    from cat in db.Categories
                    from pCat in db.PostsCategories
                    where
                        cat.Id == pCat.CategoryId &&
                        pCat.PostId == postId
                    select cat;

                return categories.ToList();
            }
        }
    }
}
