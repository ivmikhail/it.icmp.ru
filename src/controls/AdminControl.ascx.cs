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
using ITCommunity;

namespace ITCommunity
{
    public partial class AdminControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentUser.User.Role == User.Roles.Admin)
                {
                    this.Visible = true;
                }
            }
        }
        protected void LinkButtonExit_Click(object sender, EventArgs e)
        {
            CurrentUser.LogOut();
            Response.Redirect("Default.aspx");
        }
    }
}