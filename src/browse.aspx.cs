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
using ITCommunity;
using System.Diagnostics;

public partial class browse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String dir = Request.QueryString["dir"] ?? "";
        String linkTypeQuery = Request.QueryString["cat"] ?? "files";
        LinkType linkType = LinkType.Files;
        try {
            linkType = (LinkType)Enum.Parse(linkType.GetType(), linkTypeQuery, true);
        } catch(ArgumentException ex) {
            Debug.Print(ex.Message);
            linkType = LinkType.Files;
        }
        bool isViewRootDir = dir == "";

        dir = BrowseItem.GetRealPathOfLink(linkType, dir);
        if (dirOk(dir))
        {
            rptFiles.DataSource = BrowseItem.GetList(dir, isViewRootDir);
            rptFiles.DataBind();
        }
        else
        {
            //throw new Exception("Error reading directory: " + dir);
            return;
        }
    }

    private bool dirOk(string dir) {
        if(!Directory.Exists(dir)) {
            return false;
        }
        dir = Path.GetFullPath(dir);
        if(dir.StartsWith(Global.ConfigStringParam("FilesFolder"))) {
            return true;
        }
        return false;
    }

}
