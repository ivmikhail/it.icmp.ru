using System.Collections.Generic;
using System.Linq;

using ITCommunity.Db;

namespace ITCommunity.Db.Tables {

    public static class MenuItems {

        public static List<MenuItem> GetRoot() {
            using (var db = Database.Connect()) {
                var items =
                    from item in db.MenuItems
                    where item.ParentId == 0
                    orderby item.Sort ascending
                    select item;

                return items.ToList();
            }
        }

        public static List<MenuItem> GetChilds(int itemId) {
            using (var db = Database.Connect()) {
                var childs =
                    from item in db.MenuItems
                    where item.ParentId == itemId
                    orderby item.Sort ascending
                    select item;

                return childs.ToList();
            }
        }
    }
}
