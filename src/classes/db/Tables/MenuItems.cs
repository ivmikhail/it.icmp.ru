using System.Collections.Generic;
using System.Linq;

using ITCommunity.Core;


namespace ITCommunity.DB.Tables {

    public static class MenuItems {

        public const string CACHE_KEY = "MenuItems";

        public static List<MenuItem> All {
            get { return AppCache.Get(CACHE_KEY, GetAll); }
        }

        public static MenuItem Add(MenuItem menuItem) {
            using (var db = Database.Connect()) {
                db.MenuItems.InsertOnSubmit(menuItem);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
                return menuItem;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var menuItem = (
                    from itm in db.MenuItems
                    where itm.Id == id
                    select itm
                ).SingleOrDefault();

                db.MenuItems.DeleteOnSubmit(menuItem);
                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static void Update(MenuItem editedMenuItem) {
            using (var db = Database.Connect()) {
                var menuItem = (
                    from itm in db.MenuItems
                    where itm.Id == editedMenuItem.Id
                    select itm
                ).SingleOrDefault();

                menuItem.ParentId = editedMenuItem.ParentId;
                menuItem.Name = editedMenuItem.Name;
                menuItem.Url = editedMenuItem.Url;
                menuItem.Sort = editedMenuItem.Sort;
                menuItem.IsTargetBlank = editedMenuItem.IsTargetBlank;

                db.SubmitChanges();

                AppCache.Remove(CACHE_KEY);
            }
        }

        public static List<MenuItem> GetAll() {
            using (var db = Database.Connect()) {
                return db.MenuItems.ToList();
            }
        }

        public static MenuItem Get(int id) {
            var item = (
                from itm in All
                where itm.Id == id
                select itm
            ).SingleOrDefault();

            return item;
        }

        public static List<MenuItem> GetRoot() {
            var items =
                from itm in All
                where itm.ParentId == 0
                orderby itm.Sort ascending
                select itm;

            return items.ToList();
        }

        public static List<MenuItem> GetChildren(int parentId) {
            var childs =
                from itm in All
                where itm.ParentId == parentId
                orderby itm.Sort ascending
                select itm;

            return childs.ToList();
        }
    }
}
