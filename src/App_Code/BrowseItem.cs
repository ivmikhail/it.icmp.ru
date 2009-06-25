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
    public enum LinkType { Files, Books }
    public class BrowseItem {
        public const String FolderIcon = "folderclosed.gif";
        public const String ExeIcon = "exe.ico";
        public const String AnyIcon = "any.ico";
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
        private String _link;
        public String Link {
            get { return _link; }
            set { _link = value; }
        }
        private String _icon;
        public String Icon {
            get { return _icon; }
            set { _icon = value; }
        }
        private String _size;
        public String Size {
            get { 
                return _size; 
            }
            set { _size = value; }
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
        private BrowseItem(bool isDir, String link, String name) {
            this._isDir = isDir;
            this._link = link;
            this._name = name;
        }
        public static List<BrowseItem> GetList(String dir) {
            List<BrowseItem> result = new List<BrowseItem>();

            String[] dirs = Directory.GetDirectories(dir);
            for (int i = 0; i < dirs.Length; i++) {
                BrowseItem bi = new BrowseItem(true, getLinkOfDir(dirs[i]), dirs[i].Substring(dirs[i].LastIndexOf("\\") + 1));
                bi.Icon = FolderIcon;
                result.Add(bi);
            }
            
            String[] files = Directory.GetFiles(dir);
            for (int i = 0; i < files.Length; i++) {
                FileInfo fi = new FileInfo(files[i]);
                BrowseItem bi = new BrowseItem(false, getLinkOfPath(files[i]), Path.GetFileName(files[i]));
                bi.Size = setHumanSize(fi.Length);
                bi.Icon = getIcon(fi.Extension);
                result.Add(bi);
            }
            return result;

        }

        private static string getLinkOfDir(string dir) {
            String link = dir.Replace(Global.ConfigStringParam("FilesFolder"), "").Replace("\\", "/");
            String cat = link.Substring(0, link.IndexOf("/"));
            String d = link.Substring(link.IndexOf("/"));
            link = "browse.aspx?dir=" + d + "&cat=" + cat;
            return link;
        }

        private static string getIcon(string extention) {
            switch(extention) {
                case "exe":
                    return ExeIcon;
                default:
                    return AnyIcon;
            }
        }

        private static string setHumanSize(long p) {
            if(p>1000000) {
                return Math.Round(p / 1000000f, 2) + "Mb";
            } else if(p>1000) {
                return Math.Round(p / 1000f, 2) + "Kb";
            } else {
                return p.ToString();
            }
            
        }
        private static String getLinkOfPath(String path) {
            String link = path.Replace(Global.ConfigStringParam("FilesFolder"), "");
            return Global.ConfigStringParam("FilesLink") + "/" + link.Replace("\\", "/");
        }
        public static String GetRealPathOfLink(LinkType linkType, String link) {
            if(!link.StartsWith("/")) {
                link = "/" + link;
            }
            String path = Global.ConfigStringParam("FilesFolder") + Enum.GetName(linkType.GetType(), linkType) + link.Replace("/", "\\");
            return path;
        }
    }
}