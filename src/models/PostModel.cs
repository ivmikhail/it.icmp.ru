using ITCommunity.Util;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using ITCommunity.Core;

namespace ITCommunity.Models {

    public class AddPostModel {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Text { get; set; }

        public string Source { get; set; }

        public List<int> CategoriesIds { get; set; }

        public List<Category> Categories {
            get {
                if (CategoriesIds == null) {
                    return new List<Category>();
                }

                return (
                    from cat in ITCommunity.Db.Tables.Categories.GetAll()
                    where CategoriesIds.Contains(cat.Id)
                    select cat
                ).ToList();
            }
        }
    }
}
