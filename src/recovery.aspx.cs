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
	public partial class Recovery : System.Web.UI.Page
	{
		private User recovery_user;
		protected void Page_Load(object sender, EventArgs e)
		{
			string identifier = GetIdentifier();
			if (identifier == "")
			{
				SendRecoveryLink.Visible = true;
			}
			else
			{
				RecoveryPass rec = RecoveryPass.GetByIdentifier(identifier.Trim());
				if (rec.Id > 0)
				{
					recovery_user = rec.User;
					RecoveryLogin.Text = recovery_user.Nick;
					RecoveryPassContainer.Visible = true;
				}
				else
				{
					Response.Clear();
					Response.StatusCode = 404;
					Response.End();
				}
			}
		}
		protected void LinkButtonSendEmail_Click(object sender, EventArgs e)
		{
			User user = ITCommunity.User.GetByLogin(TextBoxLogin.Text.Trim());
			if (user.Id > 0)
			{
				string identifier = Guid.NewGuid().ToString("N");
				bool sended = SendEmail.SendRecoveryEmail(user, identifier);
				string message = "<div class='message'>URL для сброса пароля отправлен на указанный при регистрации e-mail</div>";
				if (sended)
				{
					RecoveryPass.Add(identifier, user.Id);
				}
				else
				{
					message = "<div class='error'>Письмо не отправлено. Причина записана в логах (увы вам она не видна). Попробуйте еще раз. Если все равно не работает, обратитесь к администрации.</div>";
				}
				SendRecoveryErrors.Text = message;
				TextBoxLogin.Text = "";
			}
			else
			{
				SendRecoveryErrors.Text = "<div class='error'>Пользователь с таким логином не зарегистрирован.</div>";
			}
		}
		protected void LinkButtonChangePass_Click(object sender, EventArgs e)
		{
			if (NewPass.Text.Length > 1 && NewPass.Text.Trim() == NewPassConfirm.Text.Trim())
			{
				recovery_user.Pass = CurrentUser.HashPass(NewPass.Text.Trim(), recovery_user.Nick);
				recovery_user.Update();
				Response.Redirect("default.aspx");
			}
		}
		private string GetIdentifier()
		{
			string res = Request.QueryString["id"];
			return res == null ? "" : res;
		}
	}
}
