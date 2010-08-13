using System.ComponentModel.DataAnnotations;

using ITCommunity.Models.Captcha;
using System.Collections.Generic;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using System.Web;
using System.Web.SessionState;


namespace ITCommunity.Models.Post {

    public class EditCategoriesModel {

        public const string SESSION_NAME = "AddCategoriesModel";

        public Dictionary<int, bool> IsAttached;

        public EditCategoriesModel() {
            IsAttached = new Dictionary<int, bool>();
            foreach (var category in Categories.GetAll()) {
                IsAttached.Add(category.Id, false);
            }
        }

        public static EditCategoriesModel Current {
            get {
                if (HttpContext.Current.Session[SESSION_NAME] == null) {
                    HttpContext.Current.Session[SESSION_NAME] = new EditCategoriesModel();
                }

                return (EditCategoriesModel)HttpContext.Current.Session[SESSION_NAME];
            }
            set {
                HttpContext.Current.Session[SESSION_NAME] = value;
            }
        }
    }
}
