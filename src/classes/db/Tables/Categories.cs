using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.Db.Tables {

    public static class Categories {

        public const string CATEGORIES_KEY = "Categories";

        public static List<Category> GetAll() {
            using (var db = Database.Connect()) {
                var categories =
                    from category in db.Categories
                    orderby category.Sort ascending
                    select category;

                return categories.ToList();
            }
        }

        public static Category Get(int id) {
            var categories = AppCache.Get(CATEGORIES_KEY, () => GetAll());

            foreach (var category in categories) {
                if (category.Id == id) {
                    return category;
                }
            }

            return null;
        }

        public static List<Category> GetByPost(int postId) {
            using (var db = Database.Connect()) {
                var categories =
                    from category in db.Categories
                    from postCategory in db.PostsCategories
                    where
                        category.Id == postCategory.CategoryId &&
                        postCategory.PostId == postId
                    select category;

                return categories.ToList();
            }
        }

        public static int GetPostsCount(int id) {
            using (var db = Database.Connect()) {
                var result =
                    from postcat in db.PostsCategories
                    where postcat.CategoryId == id
                    select postcat;

                return result.Count();
            }
        }
    }
}
