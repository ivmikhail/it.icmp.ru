using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Diagnostics;

using ITCommunity;
using System.Collections.Generic;

namespace ITCommunity {
    public partial class Browse : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            String dir = Request.QueryString["dir"] ?? "";
            String linkTypeQuery = Request.QueryString["cat"] ?? "files";
            LinkType linkType = LinkType.Files;
            try {
                linkType = (LinkType)Enum.Parse(linkType.GetType(), linkTypeQuery, true);
            } catch (ArgumentException ex) {
                //Debug.Print(ex.Message);
                Logger.Log.Info("Неправильный queryString при обращении к браузеру файлов, пользователь - " + CurrentUser.User.Login + "(" + CurrentUser.Ip + ")", ex);
                linkType = LinkType.Files;
            }
            bool isViewRootDir = dir == "";
            String path = isViewRootDir ? "/" : dir;
            String[] pathes = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            dir = BrowseItem.GetRealPathOfLink(linkType, dir);
            if (dir != null) {
                rptPath.DataSource = getPathItems(linkType, pathes);
                rptPath.DataBind();

                rptFiles.DataSource = BrowseItem.GetList(dir, isViewRootDir);
                rptFiles.DataBind();
            } else {
                Response.Redirect("notfound.aspx");
                //Context.RewritePath("notfound.aspx");
                //lblInfo.Text = "Ссылка не найдена, возможно файл был удален или перемещен в другое место.";
                //throw new Exception("Error reading directory: " + dir);
                // return;
            }
        }

        private List<BrowseItem> getPathItems(LinkType linkType, string[] pathes) {
            List<BrowseItem> result = new List<BrowseItem>(pathes.Length);
            String path = "";
            for (int i = 0; i < pathes.Length; i++) {
                path +=  pathes[i] + "/";
                BrowseItem bi = BrowseItem.Get(BrowseItem.GetRealPathOfLink(linkType, path));
                if (i == (pathes.Length - 1)) {
                    bi.Link = "";
                }
                result.Add(bi);
            }
            return result;
        }
    }
}
