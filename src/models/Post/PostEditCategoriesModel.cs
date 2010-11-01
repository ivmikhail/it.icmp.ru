using System.Collections.Generic;
using System.Web;

using ITCommunity.DB.Tables;


namespace ITCommunity.Models {

    public class PostEditCategoriesModel {

        public const string SESSION_NAME = "PostEditCategories";

        public Dictionary<int, bool> IsAttached;

        public PostEditCategoriesModel() {
            IsAttached = new Dictionary<int, bool>();
            foreach (var category in Categories.All) {
                IsAttached.Add(category.Id, false);
            }
        }

        public static PostEditCategoriesModel Current {
            get {
                if (HttpContext.Current.Session[SESSION_NAME] == null) {
                    HttpContext.Current.Session[SESSION_NAME] = new PostEditCategoriesModel();
                }

                return (PostEditCategoriesModel)HttpContext.Current.Session[SESSION_NAME];
            }
            set {
                HttpContext.Current.Session[SESSION_NAME] = value;
            }
        }

        public void Clear() {
            HttpContext.Current.Session.Remove(SESSION_NAME);
        }
    }
}
