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
	public partial class Register : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (CurrentUser.isAuth)
			{
				RegisterPanel.Visible = false;
				YetRegisteredText.Visible = true;
			}
		}

		protected void RegisterButton_Click(object sender, EventArgs e)
		{
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
					Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false));
				}
				else
				{
					RegisterFailed.IsValid = false;
				}
			}
			else
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
			if (login == "anonymous")
			{
				status = AnonymousAccount.IsValid = false;
			}
			else
			{
				ITCommunity.User user = ITCommunity.User.GetByLogin(login);
				if (user.Id > 0)
				{
					status = AccountExist.IsValid = false;
				}
			}
			return status;
		}
		private bool EmailIsValid()
		{
			bool status = true;

			string email = TextBoxEmail.Text.Trim();
			ITCommunity.User user = ITCommunity.User.GetByEmail(email);
			if (user.Id > 0)
			{
				status = AccountExist.IsValid = false;
			}

			return status;
		}
	}
}
