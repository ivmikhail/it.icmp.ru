using System.Collections.Generic;

using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

    public partial class MenuItem {

        public List<MenuItem> Children {
            get { return MenuItems.GetChildren(Id); }
        }
    }
}
