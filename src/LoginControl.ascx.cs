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

public partial class LoginControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void AspLoginControl_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (CurrentUser.LogIn(AspLoginControl.UserName, AspLoginControl.Password))
        {
            FormsAuthentication.RedirectFromLoginPage(AspLoginControl.UserName, AspLoginControl.RememberMeSet);
        }
        else
        {
            AspLoginControl.FailureText = "Неправильный логин/пароль";
        }
    }
}
