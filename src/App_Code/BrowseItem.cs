using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Summary description for BrowseItem
/// </summary>
namespace ITCommunity {
    public class BrowseItem {

        private bool _isDir;
        public bool IsDir {
            get { return _isDir; }
            set { _isDir = value; }
        }
        private String _name;
        public String Name {
            get { return _name; }
            set { _name = value; }
        }
        private String _description;
        public String Description {
            get {
                return _description;
            }
            set {
                _description = value;
            }
        }
        private BrowseItem(bool isDir, String name) {
            this._isDir = isDir;
            this._name = name;
        }
        public static List<BrowseItem> GetList(String dir) {
            List<BrowseItem> result = new List<BrowseItem>();

            String[] dirs = Directory.GetDirectories(dir);
            for (int i = 0; i < dirs.Length; i++) {
                BrowseItem bi = new BrowseItem(true, dirs[i]);
                result.Add(bi);
            }
            String[] files = Directory.GetFiles(dir);
            for (int i = 0; i < files.Length; i++) {
                BrowseItem bi = new BrowseItem(false, files[i]);
                result.Add(bi);
            }
            return result;

        }
    }
}