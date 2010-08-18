using System.Collections.Generic;

using ITCommunity.Db.Tables;


namespace ITCommunity.Db {

    public partial class MenuItem {

        public List<MenuItem> Childs {
            get {
                return MenuItems.GetChilds(Id);
            }
        }
    }
}
