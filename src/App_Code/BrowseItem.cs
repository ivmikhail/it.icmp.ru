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
using System.Text;

/// <summary>
/// Summary description for BrowseItem
/// </summary>
namespace ITCommunity {
    public enum LinkType { Files, Books }
    public class BrowseItem {
        public const String FolderIcon = "folderclosed.gif";
        public const String ExeIcon = "exe.ico";
        public const String AnyIcon = "any.ico";
        public const String UpIcon = "up.ico";
        private const String descriptionFile = "descript.ion";
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
        public static BrowseItem Get(String dir) {
            BrowseItem bi = new BrowseItem(true, getLinkOfDir(dir), getDirName(dir));
            return bi;
        }
        public static List<BrowseItem> GetList(String dir, bool isViewRoot) {

            List<BrowseItem> result = new List<BrowseItem>();
            Dictionary<String, String> descriptions = getDescriptions(dir);
            if(!isViewRoot) {
                DirectoryInfo di = Directory.GetParent(dir);

                BrowseItem bi = new BrowseItem(true, getLinkOfDir(di.Parent.FullName), "..");
                bi.Icon = UpIcon;
                bi.Description = "Выше";
                result.Add(bi);
            }
            
            String[] dirs = Directory.GetDirectories(dir);
            for (int i = 0; i < dirs.Length; i++) {
                String dirName = getDirName(dirs[i]);
                BrowseItem bi = Get(dirs[i]);
                bi.Icon = FolderIcon;
                descriptions.TryGetValue(dirName, out bi._description);
                result.Add(bi);
            }
            String[] files = Directory.GetFiles(dir);
            for (int i = 0; i < files.Length; i++) {
                FileInfo fi = new FileInfo(files[i]);
                if(fi.Name.ToLower()==descriptionFile) {
                    continue;
                }
                BrowseItem bi = new BrowseItem(false, getLinkOfPath(files[i]), fi.Name);
                bi.Size = setHumanSize(fi.Length);
                bi.Icon = getIcon(fi.Extension);
                descriptions.TryGetValue(fi.Name, out bi._description);
                
                result.Add(bi);
            }
            return result;

        }

        private static String getDirName(String dir) {
            if (dir[dir.Length - 1] == '\\') {
                dir = dir.Substring(0, dir.Length - 1);
            }
            return dir.Substring(dir.LastIndexOf("\\") + 1);
        }
        private static Dictionary<String, String> getDescriptions(String dir) {
            Dictionary<String, String> descriptions = new Dictionary<string, string>();
            if (File.Exists(dir + descriptionFile)) {
                String[] descs = File.ReadAllLines(dir + descriptionFile, Encoding.GetEncoding(866));
                foreach (String line in descs) {
                    String fname = null;
                    String desc = null;
                    int delimeter = -1;
                    if (line.Length>2 && line[0] == '\"') {
                        delimeter = line.IndexOf("\"", 1);
                        if (delimeter > 0) {
                            fname = line.Substring(1, delimeter - 1);
                            delimeter = line.IndexOf(" ", delimeter);
                            if (delimeter > 0) {
                                desc = line.Substring(delimeter + 1);
                            }
                        }
                    } else {
                        delimeter = line.IndexOf(" ");
                        if (delimeter > 0) {
                            fname = line.Substring(0, delimeter);
                            desc = line.Substring(delimeter + 1);
                        }
                    }
                    if (fname != null && desc != null) {
                        descriptions.Add(fname, desc);
                    }
                }
            }
            return descriptions;
        }
        private static string getLinkOfDir(string dir) {

            String link = dir.Replace(Global.ConfigStringParam("FilesFolder"), "").Replace("\\", "/");
            int i = link.IndexOf("/");
            if (i == -1) {
                i = link.Length;
            }
            String cat = link.Substring(0, i);
            String d = Uri.EscapeDataString(link.Substring(i));
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
            return Uri.EscapeDataString(Global.ConfigStringParam("FilesLink") + "/" + link.Replace("\\", "/"));
        }
        /// <summary>
        /// Вычисляет реальный путь к папке на диске
        /// </summary>
        /// <param name="linkType"></param>
        /// <param name="link"></param>
        /// <returns>null, если путь нехороший</returns>
        public static String GetRealPathOfLink(LinkType linkType, String link) {
            if(!link.StartsWith("/")) {
                link = "/" + link;
            }
            link = Uri.UnescapeDataString(link);
            String pathBegin = Global.ConfigStringParam("FilesFolder") + Enum.GetName(linkType.GetType(), linkType);
            String path = pathBegin + link.Replace("/", "\\");
            if(!path.EndsWith("\\")) {
                path = path + "\\";
            }
            path = Path.GetFullPath(path);
            if (!Directory.Exists(path)) {
                return null;
            }
            if (!path.StartsWith(pathBegin, StringComparison.CurrentCultureIgnoreCase)) {
                return null;
            }
            return path;
        }
    }
}