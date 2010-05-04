using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ITCommunity {

	public partial class Browse : Page {

		protected void Page_Load(object sender, EventArgs e) {
			string dir = Request.QueryString["dir"] ?? "";
			string linkTypeQuery = Request.QueryString["cat"] ?? "files";
			LinkType linkType = LinkType.Files;
			try {
				linkType = (LinkType)Enum.Parse(linkType.GetType(), linkTypeQuery, true);
			}
			catch (ArgumentException ex) {
//				Debug.Print(ex.Message);
				Logger.Log.Info("Неправильный queryString при обращении к браузеру файлов, пользователь - " + CurrentUser.User.Login + "(" + CurrentUser.Ip + ")", ex);
				linkType = LinkType.Files;
			}
			dir = unescapeLink(dir);
			bool isViewRootDir = dir == "/";
			string path = isViewRootDir ? "/" : dir;
			string[] pathes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
			dir = BrowseItem.GetRealPathOfLink(linkType, dir);

			if (dir != null) {
				if (isViewRootDir) {
					hrefRoot.Visible = false;
				}
				else {
					hrefRoot.Visible = true;
					BrowseItem rootDirInfo = BrowseItem.Get(BrowseItem.GetRealPathOfLink(linkType, ""));
					hrefRoot.HRef = rootDirInfo.Link;
				}
				rptPath.DataSource = getPathItems(linkType, pathes);
				rptPath.DataBind();

				rptFiles.DataSource = BrowseItem.GetList(dir, isViewRootDir);
				rptFiles.DataBind();
			}
			else {
				Response.Redirect("notfound.aspx");
				//Context.RewritePath("notfound.aspx");
				//lblInfo.Text = "Ссылка не найдена, возможно файл был удален или перемещен в другое место.";
				//throw new Exception("Error reading directory: " + dir);
				// return;
			}
		}

		private List<BrowseItem> getPathItems(LinkType linkType, string[] pathes) {
			List<BrowseItem> result = new List<BrowseItem>(pathes.Length);
			string path = "";
			for (int i = 0; i < pathes.Length; i++) {
				path += pathes[i] + "/";
				BrowseItem bi = BrowseItem.Get(BrowseItem.GetRealPathOfLink(linkType, path));
				if (i == (pathes.Length - 1)) {
					bi.Link = "";
				}
				result.Add(bi);
			}
			return result;
		}

		private string unescapeLink(string link) {
			string result;
			try {
				result = Uri.UnescapeDataString(link);
			}
			catch (UriFormatException e) {
				Logger.Log.Error("Кажется нас пытаются хакнуть", e);
				result = "/";
			}
			return result;
		}
	}
}
