using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace ITCommunity.Core {

    /// <summary>
    /// Класс для отображения файлов
    /// </summary>
    public class BrowseItem {

        private const string DESCRIPTION_FILENAME = "descript.ion";

        public static string DefaultDir {
            get { return Config.Get("BrowserDefaultDirectory"); }
        }

        public static string BasePath {
            get { return Config.Get("BrowserBasePath").Trim('\\') + '\\'; }
        }

        public static string BaseUrl {
            get { return Config.Get("BrowserBaseUrl").Trim('/') + '/'; }
        }

        public static List<BrowseItem> Root {
            get { return GetChildren(BasePath, true); }
        }

        private string _path;

        public bool IsRoot { get; set; }
        public bool IsDir { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }

        public string Link {
            get {
                var relative = _path.Replace(BasePath, "").Replace('\\', '/');
                return IsDir ? relative : BaseUrl + relative;
            }
        }

        public List<BrowseItem> Children {
            get { return GetChildren(_path, false); }
        }

        public List<BrowseItem> Parents {
            get { return GetParents(_path); }
        }

        public BrowseItem Parent {
            get { return GetParent(_path); }
        }

        private BrowseItem(string path, bool isDir) {
            _path = path;
            IsDir = isDir;

            Initialize();
        }

        private void Initialize() {
            if (IsDir) {
                Name = _path.Substring(_path.LastIndexOf('\\') + 1);
            } else {
                var info = new FileInfo(_path);
                Name = info.Name;
                Extension = info.Extension;
                Size = info.Length;
            }
        }

        public static BrowseItem GetByLink(string link) {
            var path = GetRealPath(link);

            if (path != null) {
                return new BrowseItem(path, true);
            }

            return null;
        }

        /// <summary>
        /// Вычисляет реальный путь к папке на диске
        /// </summary>
        /// <param name="link">unescaped link, пример "video/adobe"</param>
        /// <returns>null, если путь нехороший</returns>
        private static string GetRealPath(string link) {
            link = link ?? DefaultDir;
            var path = link.Replace('/', '\\');

            try {
                path = Uri.UnescapeDataString(path);
            } catch (UriFormatException e) {
                path = DefaultDir;
                Logger.Log.Error("Кажется нас пытаются хакнуть", e);
            }

            var fullPath = BasePath + path;
            fullPath = Path.GetFullPath(fullPath);

            if (Directory.Exists(fullPath) == false) {
                return null;
            }
            if (fullPath.StartsWith(BasePath, StringComparison.CurrentCultureIgnoreCase) == false) {
                Logger.Log.Info("Пытаются зайти сюда: " + fullPath);
                return null;
            }

            return fullPath;
        }

        private static List<BrowseItem> GetChildren(string path, bool onlyDirs) {
            var result = new List<BrowseItem>();
            var descriptions = GetDescriptions(path);

            var dirs = Directory.GetDirectories(path);
            foreach (var directory in dirs) {
                var item = new BrowseItem(directory, true);
                item.Description = descriptions.ContainsKey(item.Name) ? descriptions[item.Name] : null;
                result.Add(item);
            }

            if (onlyDirs == false) {
                var files = Directory.GetFiles(path);
                foreach (var file in files) {
                    if (file.EndsWith('\\' + DESCRIPTION_FILENAME)) {
                        continue;
                    }
                    var item = new BrowseItem(file, false);
                    item.Description = descriptions.ContainsKey(item.Name) ? descriptions[item.Name] : null;
                    result.Add(item);
                }
            }

            return result;
        }

        private static List<BrowseItem> GetParents(string path) {
            var result = new List<BrowseItem>();

            var item = GetParent(path);
            while (item != null) {
                result.Add(item);
                item = item.Parent;
            }

            result.Reverse();
            return result;
        }

        private static BrowseItem GetParent(string path) {
            var start = path.LastIndexOf('\\');
            var parentPath = path.Remove(start);

            if (BasePath.StartsWith(parentPath)) {
                return null;
            }

            return new BrowseItem(parentPath, true);
        }

        private static Dictionary<string, string> GetDescriptions(string path) {
            var descriptions = new Dictionary<string, string>();

            var descPath = path + '\\' + DESCRIPTION_FILENAME;

            if (File.Exists(descPath)) {
                string[] descs = File.ReadAllLines(descPath, Encoding.GetEncoding(866));
                foreach (var line in descs) {
                    string fname = null;
                    string desc = null;
                    int delimeter = -1;
                    if (line.Length > 2 && line[0] == '\"') {
                        delimeter = line.IndexOf('\"', 1);
                        if (delimeter > 0) {
                            fname = line.Substring(1, delimeter - 1);
                            delimeter = line.IndexOf(' ', delimeter);
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
    }
}
