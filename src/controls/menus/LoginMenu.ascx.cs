using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ITCommunity {
	public partial class LoginMenu : UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			TextBoxPass.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) __doPostBack('" + LogInButton.UniqueID + "','')");
			if (!IsPostBack) {
				Visible = !CurrentUser.isAuth;
			}
		}

		//TODO: хммм
		protected void LogInButton_Click(object sender, EventArgs e) {
			string login = TextBoxLogin.Text;
			string pass = TextBoxPass.Text;
			bool remember = CheckBoxIsRemember.Checked;

            List<string> errors = Validate();
			if (errors.Count == 0) {
				if (CurrentUser.LogIn(login, pass, remember)) {
					if (CurrentUser.User.Role == User.Roles.Banned) {
						errors.Add("Ваш аккаунт забанен. Вы не можете авторизоваться");
						CurrentUser.LogOut();
					} else {
						//Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false));
						string targetUrl = Request.Params["ReturnUrl"] == null ? Request.Url.OriginalString : Request.Params["ReturnUrl"];
						Response.Redirect(targetUrl);
					}
				} else {
					errors.Add("Неправильный логин/пароль");
				}
			}

			WriteErrors(errors);
		}

		private List<string> Validate() {
			
		    List<string> errors = new List<string>();

			if (TextBoxLogin.Text == "") {
				errors.Add("Введите логин");
			}
			if (TextBoxPass.Text == "") {
				errors.Add("Введите пароль");
			}

			return errors;
		}

		private void WriteErrors(List<string> errors) {
			string text = "<div class=\"error\"><ul>";
			foreach (string err in errors) {
				text += "<li>" + err + "</li>";
			}
			text += "</ul></div>";
			Errors.Text = text;
		}
	}
}
