using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ITCommunity {
	public partial class LoginMenu : UserControl {

		private List<string> _errors = new List<string>();

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

			if (Validate()) {
				if (CurrentUser.LogIn(login, pass, remember)) {
					if (CurrentUser.User.Role == User.Roles.Banned) {
						_errors.Add("Ваш аккаунт забанен. Вы не можете авторизоваться");
						CurrentUser.LogOut();
					}
					else {
						//Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false));
						string targetUrl = Request.Params["ReturnUrl"] == null ? Request.Url.OriginalString : Request.Params["ReturnUrl"];
						Response.Redirect(targetUrl);
					}
				}
				else {
					_errors.Add("Неправильный логин/пароль");
				}
			}

			WriteErrors();
		}

		private bool Validate() {
			bool result = true;

			if (TextBoxLogin.Text == "") {
				_errors.Add("Введите логин");
				result = false;
			}
			if (TextBoxPass.Text == "") {
				_errors.Add("Введите пароль");
				result = false;
			}

			return result;
		}

		private void WriteErrors() {
			string text = "<div class=\"error\"><ul>";
			foreach (string error in _errors) {
				text += "<li>" + error + "</li>";
			}
			text += "</ul></div>";
			Errors.Text = text;
		}
	}
}
