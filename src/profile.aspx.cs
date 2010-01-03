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
	public partial class ProfilePage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				TextBoxEmail.Text = CurrentUser.User.Email;
			}
		}
		protected void EditProfileButton_Click(object sender, EventArgs e)
		{
			User current = CurrentUser.User;

			if (AllIsValid())
			{
				current.Email = TextBoxEmail.Text.Trim();
				if (TextBoxNewPass.Text.Trim() != "")
				{
					current.Pass = CurrentUser.HashPass(TextBoxNewPass.Text, current.Login);
				}
				current.Update();
				LiteralUpdatedMessage.Text = "<div class='message'>Изменения вступили в силу.</div>";
			}
		}

		private bool AllIsValid()
		{
			return PassIsValid() && ProfileIsValid() && EmailIsValid();
		}

		private bool ProfileIsValid()
		{
			RequiredEmail.Validate();
			EmailValidator.Validate();
			ConfirmPassword.Validate();

			return RequiredEmail.IsValid &&
					EmailValidator.IsValid &&
					ConfirmPassword.IsValid;
		}

		private bool EmailIsValid()
		{
			bool status = true;
			if (CurrentUser.User.Email != TextBoxEmail.Text.Trim())
			{
				string email = TextBoxEmail.Text.Trim();
				User user = ITCommunity.User.GetByEmail(email);
				if (user.Id > 0)
				{
					status = EmailExist.IsValid = false;
				}
			}
			return status;
		}

		private bool PassIsValid()
		{
			bool status = true;

			if (TextBoxNewPassConf.Text.Trim() != TextBoxNewPass.Text.Trim())
			{
				status = ConfirmPassword.IsValid = false;
			}

			User current = CurrentUser.User;

			if (current.Pass != CurrentUser.HashPass(TextBoxPassConf.Text, current.Login))
			{
				status = OldPassConfirm.IsValid = false;
			}

			return status;
		}
	}
}
