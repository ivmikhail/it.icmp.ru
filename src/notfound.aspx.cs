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

namespace ITCommunity
{
    public partial class NotFoundPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.StatusCode = 404;
            if (!IsPostBack)
            {
                string req_path = Request.QueryString["aspxerrorpath"];
                if(!string.IsNullOrEmpty(req_path))
                {
                    LiteralReferrerUrl.Text = "Запрошенный ресурс - " + Global.SiteAddress + req_path;
                }
            }
        }
    }
}
