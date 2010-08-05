using ITCommunity.Core;
using ITCommunity.Util;
using ITCommunity.Db.Tables;
using System.Web;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;

namespace ITCommunity.Db {

    public partial class Category {

        public int PostsCount {
            get {
                using (var db = Database.Connect()) {
                    var result =
                        from postcat in db.PostsCategories
                        where postcat.CategoryId == Id
                        select postcat;

                    return result.Count();
                }
            }
        }

    }
}
