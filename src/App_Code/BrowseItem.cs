using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ITCommunity {

	public enum LinkType {
		Files,
		Books
	}

	/// <summary>
	/// Summary description for BrowseItem
	/// </summary>
	public class BrowseItem {

		#region Icons

		public const string FolderIcon = "folderclosed.gif";
		public const string ExeIcon = "exe.ico";
		public const string AnyIcon = "any.ico";
		public const string UpIcon = "up.ico";
		private const string descriptionFile = "descript.ion";

		#endregion

		#region Properties

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
			get {
				return _size;
			}
			set { _size = value; }
		}

		public string Description {
			get {
				return _description;
			}
			set {
				_description = value;
			}
		}

		#endregion

		private BrowseItem(bool isDir, string link, string name) {
			_isDir = isDir;
			_link = link;
			_name = name;
		}

		public static BrowseItem Get(string dir) {
			return new BrowseItem(true, GetLinkOfDir(dir), GetDirName(dir));
		}

		public static List<BrowseItem> GetList(string dir, bool isViewRoot) {
			List<BrowseItem> result = new List<BrowseItem>();
			Dictionary<string, string> descriptions = GetDescriptions(dir);

			if (!isViewRoot) {
				DirectoryInfo di = Directory.GetParent(dir);
				BrowseItem bi = new BrowseItem(true, GetLinkOfDir(di.Parent.FullName), "..");
				bi.Icon = UpIcon;
				bi.Description = "Выше";
				result.Add(bi);
			}

			string[] dirs = Directory.GetDirectories(dir);
			for (int i = 0; i < dirs.Length; i++) {
				string dirName = GetDirName(dirs[i]);
				BrowseItem bi = Get(dirs[i]);
				bi.Icon = FolderIcon;
				descriptions.TryGetValue(dirName, out bi._description);
				result.Add(bi);
			}

			string[] files = Directory.GetFiles(dir);
			for (int i = 0; i < files.Length; i++) {
				FileInfo fi = new FileInfo(files[i]);
				if (fi.Name.ToLower() == descriptionFile) {
					continue;
				}
				BrowseItem bi = new BrowseItem(false, GetLinkOfPath(files[i]), fi.Name);
				bi.Size = SetHumanSize(fi.Length);
				bi.Icon = GetIcon(fi.Extension);
				descriptions.TryGetValue(fi.Name, out bi._description);
				result.Add(bi);
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
			string pathBegin = Config.String("FilesFolder") + Enum.GetName(linkType.GetType(), linkType);
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

		private static string GetDirName(string dir) {
			if (dir[dir.Length - 1] == '\\') {
				dir = dir.Substring(0, dir.Length - 1);
			}
			return dir.Substring(dir.LastIndexOf("\\") + 1);
		}

		private static Dictionary<string, string> GetDescriptions(string dir) {
			Dictionary<string, string> descriptions = new Dictionary<string, string>();

			if (File.Exists(dir + descriptionFile)) {
				string[] descs = File.ReadAllLines(dir + descriptionFile, Encoding.GetEncoding(866));
				foreach (string line in descs) {
					string fname = null;
					string desc = null;
					int delimeter = -1;
					if (line.Length > 2 && line[0] == '\"') {
						delimeter = line.IndexOf("\"", 1);
						if (delimeter > 0) {
							fname = line.Substring(1, delimeter - 1);
							delimeter = line.IndexOf(" ", delimeter);
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

		private static string GetLinkOfDir(string dir) {
			if (!dir.EndsWith("\\")) {
				dir += "\\";
			}
			string link = dir.Replace(Config.String("FilesFolder"), "").Replace("\\", "/");
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
					return ExeIcon;
				default:
					return AnyIcon;
			}
		}

		private static string SetHumanSize(long p) {
			if (p > 1000000) {
				return Math.Round(p / 1000000f, 2) + "Mb";
			}
			else if (p > 1000) {
				return Math.Round(p / 1000f, 2) + "Kb";
			}
			else {
				return p.ToString();
			}

		}

		private static string GetLinkOfPath(string path) {
			string link = path.Replace(Config.String("FilesFolder"), "");
			return Uri.EscapeDataString(Config.String("FilesLink") + "/" + link.Replace("\\", "/"));
		}
	}
}
