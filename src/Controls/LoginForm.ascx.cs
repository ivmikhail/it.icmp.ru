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
                Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false));

                //Response.Redirect("Default.aspx");
                //FormsAuthentication.RedirectFromLoginPage(login, remember);
                //System.Web.Security.FormsIdentity id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;
                //id.Ticket.UserData = Convert.ToString((int)user.Role);
                //FormsAuthentication.RenewTicketIfOld(new FormsAuthenticationTicket(1, user.Nick, DateTime.Now, ticketExpiration, remember, Convert.ToString((int)user.Role)));
            } else
            {
                WrongAccount.IsValid = false;
            }
        }
    }
}