using System.Collections.Generic;
using System.Linq;

using ITCommunity.Db.Tables;
using System.Web;

namespace ITCommunity.Db {

    public partial class MenuItem {

        public List<MenuItem> Childs {
            get {
                return MenuItems.GetChilds(Id);
            }
        }
    }
}
