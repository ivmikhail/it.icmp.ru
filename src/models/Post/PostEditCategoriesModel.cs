using System.Collections.Generic;
using System.Web;

using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class PostEditCategoriesModel {

        public const string SESSION_NAME = "AddCategoriesModel";

        public Dictionary<int, bool> IsAttached;

        public PostEditCategoriesModel() {
            IsAttached = new Dictionary<int, bool>();
            foreach (var category in Categories.GetAll()) {
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
    }
}
