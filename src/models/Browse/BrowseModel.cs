using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Core;


namespace ITCommunity.Models {

    public class BrowseModel {

        public List<BrowseItem> Files { get; set; }

        public List<BrowseItem> Pathes { get; set; }

        public BrowseItem RootDir { get; set; }        
       
        public BrowseModel(){            
        }

    }
}
