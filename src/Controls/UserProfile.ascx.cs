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

public partial class UserProfile : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LabelUserLogin.Text = CurrentUser.User.Nick;
            LabelUserRole.Text = CurrentUser.User.Role.ToString();
        }
    }
    protected void LinkButtonExit_Click(object sender, EventArgs e)
    {
        CurrentUser.LogOut();
        Response.Redirect("Default.aspx");
    }
}
