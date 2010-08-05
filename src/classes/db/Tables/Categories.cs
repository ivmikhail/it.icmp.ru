using System.Linq;
using ITCommunity.Db;
using System.Collections.Generic;
using ITCommunity.Core;
using System.Web.Mvc;

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

        public static List<SelectListItem> GetSelectCategories() {
            var result = from category in GetAll()
                         select new SelectListItem {
                             Text = category.Name,
                             Value = category.Id.ToString()
                         };

            return result.ToList();
        }
    }
}
