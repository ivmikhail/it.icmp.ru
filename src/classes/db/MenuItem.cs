using System.Collections.Generic;

using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

    public partial class MenuItem {

        public List<MenuItem> Childs {
            get {
                return MenuItems.GetChilds(Id);
            }
        }
    }
}
