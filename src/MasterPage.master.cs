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

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.isAuth)
        {
            UserProfile.Visible = true;
        } else
        {
            LoginForm.Visible = true;
        }
        Label1.Text = "����������� �� -" + Convert.ToString(CurrentUser.isAuth);
        Label1.Text += "  �����-"+ CurrentUser.User.Nick;
        Label1.Text += "  role-" + CurrentUser.User.Role;
    }
}
