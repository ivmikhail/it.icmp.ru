using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ITCommunity {

	public enum LinkType {
		Files,
		Books,
		Video,
		Audio
	}

	/// <summary>
	/// Класс для отображения файлов
	/// </summary>
	public class BrowseItem {

		#region Constants

		public const string FOLDER_ICON = "folderclosed.gif";
		public const string EXE_ICON = "exe.ico";
		public const string ANY_ICON = "any.ico";
		public const string UP_ICON = "up.ico";
		private const string DESCRIPTION_FILENAME = "descript.ion";

		#endregion

		#region Properties

		private static string _filesFolder {
			get {
				return Config.Get("FilesFolder");
			}
		}

		private bool _isDir;
		private string _name;
		private string _link;
		private string _icon;
		private string _size;
		private string _description;

		public bool IsDir {
			get { return _isDir; }
			set { _isDir = value; }
		}

		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		public string Link {
			get { return _link; }
			set { _link = value; }
		}

		public string Icon {
			get { return _icon; }
			set { _icon = value; }
		}

		public string Size {
			get { return _size; }
			set { _size = value; }
		}

		public string Description {
			get { return _description; }
			set { _description = value; }
		}

		#endregion

		#region Constructors

		private BrowseItem(bool isDir, string link, string name) {
			_isDir = isDir;
			_link = link;
			_name = name;
		}

		#endregion

		#region Public static methods

		public static BrowseItem Get(string dir) {
			return new BrowseItem(true, GetDirLink(dir), GetDirName(dir));
		}

		public static List<BrowseItem> GetList(string dir, bool isRoot) {
			var result = new List<BrowseItem>();
			var descriptions = GetDescriptions(dir);

			if (!isRoot) {
				var parent = Directory.GetParent(dir).Parent;
				var item = new BrowseItem(true, GetDirLink(parent.FullName), "..");
				item.Icon = UP_ICON;
				item.Description = "Выше";
				result.Add(item);
			}

			var dirs = Directory.GetDirectories(dir);
			foreach (var directory in dirs) {
				BrowseItem item = Get(directory);
				item.Icon = FOLDER_ICON;
				descriptions.TryGetValue(item.Name, out item._description);
				result.Add(item);
			}

			var files = Directory.GetFiles(dir);
			foreach (var file in files) {
				var info = new FileInfo(file);
				if (info.Name.ToLower() == DESCRIPTION_FILENAME) {
					continue;
				}

				var item = new BrowseItem(false, GetPathLink(file), info.Name);
				item.Size = GetHumanSize(info.Length);
				item.Icon = GetIcon(info.Extension);
				descriptions.TryGetValue(item.Name, out item._description);
				result.Add(item);
			}

			return result;
		}

		/// <summary>
		/// Вычисляет реальный путь к папке на диске
		/// </summary>
		/// <param name="linkType"></param>
		/// <param name="link">unescaped link, пример "video/adobe"</param>
		/// <returns>null, если путь нехороший</returns>
		public static string GetRealPathOfLink(LinkType linkType, string link) {
			if (!link.StartsWith("/")) {
				link = "/" + link;
			}
			string pathBegin = _filesFolder + Enum.GetName(linkType.GetType(), linkType);
			string path = pathBegin + link.Replace("/", "\\");
			if (!path.EndsWith("\\")) {
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

		#endregion

		#region Private static methods

		private static string GetDirName(string dir) {
			if (dir.EndsWith("\\")) {
				dir = dir.Remove(dir.Length - 2);
			}
			return dir.Substring(dir.LastIndexOf('\\') + 1);
		}

		private static Dictionary<string, string> GetDescriptions(string dir) {
			var descriptions = new Dictionary<string, string>();

			if (File.Exists(dir + DESCRIPTION_FILENAME)) {
				string[] descs = File.ReadAllLines(dir + DESCRIPTION_FILENAME, Encoding.GetEncoding(866));
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
					}
					else {
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

		private static string GetDirLink(string dir) {
			if (!dir.EndsWith("\\")) {
				dir += "\\";
			}
			string link = dir.Replace(_filesFolder, "").Replace("\\", "/");
			int i = link.IndexOf("/");
			if (i == -1) {
				i = link.Length;
			}
			string cat = link.Substring(0, i);
			string d = Uri.EscapeDataString(link.Substring(i));
			link = "browse.aspx?dir=" + d + "&amp;cat=" + cat;
			return link;
		}

		private static string GetIcon(string extention) {
			switch (extention) {
				case "exe":
					return EXE_ICON;
				default:
					return ANY_ICON;
			}
		}

		private static string GetPathLink(string path) {
			string link = path.Replace(_filesFolder, "");
			return Uri.EscapeDataString(Config.Get("FilesLink") + "/" + link.Replace("\\", "/"));
		}

		private static string GetHumanSize(long p) {
			if (p > 1048576) {
				return Math.Round(p / 1048576f, 2) + " MB";
			}
			if (p > 1024) {
				return Math.Round(p / 1024f, 2) + " KB";
			}
			return p.ToString() + " B";
		}

		#endregion

	}
}
