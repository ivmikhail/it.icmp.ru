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
            get { return Config.BrowserDefaultDirectory; }
        }

        public static string BasePath {
            get { return Config.BrowserBasePath.Trim('\\') + '\\'; }
        }

        public static string BaseUrl {
            get { return Config.BrowserBaseUrl.Trim('/') + '/'; }
        }

        public static List<BrowseItem> Root {
            get { return GetChildren(BasePath, true); }
        }

        private string _path;

        public bool IsRoot {
            get {
                return ParentPath.Equals(BasePath, StringComparison.CurrentCultureIgnoreCase);
            }
        }
        public bool IsDir { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string Link {
            get {
                return BaseUrl + RelativeLink;
            }
        }

        public string ShortName {
            get {
                if (Name.Length > 30) {
                    return Name.Substring(0, 27) + "...";
                }
                return Name;
            }
        }

        public string RelativeLink {
            get {
                return _path.Replace(BasePath, "").Replace('\\', '/');
            }
        }

        public string ParentPath {
            get {
                return _path.Substring(0, _path.LastIndexOf('\\'));
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
                var info = new DirectoryInfo(_path);
                Name = info.Name;
                ModifiedDate = info.LastWriteTime;
            } else {
                var info = new FileInfo(_path);
                ModifiedDate = info.LastWriteTime;
                Name = info.Name;
                Extension = info.Extension;
                Size = info.Length;
            }
        }

        public static BrowseItem GetByLink(string link) {
            var path = GetRealPath(link);

            if (path != null) {
                var item = new BrowseItem(path, true);
                if (!item.IsRoot) {
                    var descs = GetDescriptions(item.ParentPath);
                    item.Description = descs.ContainsKey(item.Name) ? descs[item.Name] : null;
                }
                return item;
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
            } catch (UriFormatException ex) {
                path = DefaultDir;
                Logger.Log.Error("Кажется нас пытаются хакнуть" + Logger.GetUserInfo(), ex);
            }

            var fullPath = BasePath + path;
            fullPath = Path.GetFullPath(fullPath);

            if (!Directory.Exists(fullPath) && !File.Exists(fullPath)) {
                return null;
            }
            if (fullPath.StartsWith(BasePath, StringComparison.CurrentCultureIgnoreCase) == false) {
                Logger.Log.Error("Пытаются зайти сюда: " + fullPath + Logger.GetUserInfo());
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
                    if (file.EndsWith('\\' + DESCRIPTION_FILENAME, StringComparison.CurrentCultureIgnoreCase)) {
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
                string[] descs = File.ReadAllLines(descPath, Encoding.UTF8);
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

        private static void SaveDescriptions(Dictionary<string, string> descriptions, string path) {
            var descPath = path + '\\' + DESCRIPTION_FILENAME;

            var descs = new List<string>();

            foreach (var desc in descriptions) {
                descs.Add("\"" + desc.Key + "\" " + desc.Value);
            }

            try {
                File.WriteAllLines(descPath, descs, Encoding.UTF8);
            } catch (Exception ex) {
                Logger.Log.Error("Произошла ошибка при записи файла", ex);
            }
        }

        public void UpdateDesciption(string desc) {
            var descriptions = GetDescriptions(ParentPath);

            if (descriptions.ContainsKey(Name)) {
                descriptions[Name] = desc;
            } else {
                descriptions.Add(Name, desc);
            }

            SaveDescriptions(descriptions, ParentPath);
        }
    }
}
