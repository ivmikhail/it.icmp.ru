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

public partial class browse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String dir = Request.QueryString["dir"] == null ? Global.FilesFolder : Global.FilesFolder + Request.QueryString["dir"].ToString();
        dir = Path.GetFullPath(dir);
        if(!dirOk(dir)) {
            return;
        }
        String[] files = Directory.GetDirectories(dir);
        DirectoryInfo di = new DirectoryInfo(dir);
        FileInfo[] fi = di.GetFiles();
        rptFiles.DataSource = files;
        rptFiles.DataBind();
    }

    private bool dirOk(string dir) {
        if(!Directory.Exists(dir)) {
            return false;
        }
        if(dir.StartsWith(Global.FilesFolder)) {
            return true;
        }
        return false;
    }

}
