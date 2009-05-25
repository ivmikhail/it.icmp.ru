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
    public partial class LoginForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        bool UserDataIsValid()
        {
            bool status = false;

            RequiredLogin.Validate();
            RequiredPass.Validate();

            if (RequiredLogin.IsValid && RequiredPass.IsValid)
            {
                status = true;
            }
            return status;
        }

        //TODO: υμμμ
        protected void LogInButton_Click(object sender, EventArgs e)
        {
            string login = TextBoxLogin.Text;
            string pass = TextBoxPass.Text;
            bool remember = CheckBoxIsRemember.Checked;
            if (!UserDataIsValid())
            {
                return;
            }
            User user = CurrentUser.LogIn(login, pass, remember);
            if (user.Id > 0)
            {
                if (user.Role == User.Roles.Banned)
                {
                    UserIsBanned.IsValid = false;
                    CurrentUser.LogOut();
                } else
                {
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false));
                }
            } else 
            {
                WrongAccount.IsValid = false;
            }
        }
    }
}