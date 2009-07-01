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

    public partial class RegUserForm : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            //TODO: Запретить регистрироваться с ником anounymous
            if (captcha.IsRightAnswer())
            {
                if (!AllIsValid())
                {
                    return;
                }

                string login = TextBoxLogin.Text.Trim();
                string pass = TextBoxPass.Text;
                string email = TextBoxEmail.Text.Trim().ToLower();

                User user = CurrentUser.Register(login, pass, email);

                if (user.Id > 0)
                {
                    CurrentUser.LogIn(login, pass, true);
                    FormsAuthentication.RedirectFromLoginPage(login, true);
                } else
                {
                    RegisterFailed.IsValid = false;
                }
            } else
            {
                captcha.SetErrorMessageVisible();
            }
        }

        private bool AllIsValid()
        {
            if (!RegIsValid())
            {
                return false;
            }
            if (TextBoxPass.Text != TextBoxPassConf.Text)
            {
                ConfirmPassword.IsValid = false;
                return false;
            }
            return LoginIsValid() && EmailIsValid();
        }

        private bool RegIsValid()
        {
            RequiredLogin.Validate();
            LoginValidator.Validate();
            RequiredEmail.Validate();
            EmailValidator.Validate();
            RequiredPass.Validate();
            ConfirmPassword.Validate();

            return RequiredLogin.IsValid &&
                    LoginValidator.IsValid &&
                    RequiredEmail.IsValid &&
                    EmailValidator.IsValid &&
                    ConfirmPassword.IsValid;
        }

        private bool LoginIsValid()
        {
            bool status = true;

            string login = TextBoxLogin.Text.Trim();
            User user = User.GetByLogin(login);
            if (user.Id > 0)
            {
                status = AccountExist.IsValid = false;
            }

            return status;
        }
        private bool EmailIsValid()
        {
            bool status = true;

            string email = TextBoxEmail.Text.Trim();
            User user = User.GetByEmail(email);
            if (user.Id > 0)
            {
                status = AccountExist.IsValid = false;
            }

            return status;
        }
    }
}