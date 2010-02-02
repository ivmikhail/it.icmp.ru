using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ITCommunity;

namespace ITCommunity
{
    public partial class LoginMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TextBoxPass.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) __doPostBack('" + LogInButton.UniqueID + "','')");
            if (!IsPostBack)
            {
                if (CurrentUser.isAuth)
                {
                    this.Visible = false;
                } else
                {
                    this.Visible = true;
                }
            }

        }
        /*
         * 	<asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateAuthData" DisplayMode="List" CssClass="error" />

	<asp:RequiredFieldValidator ID="RequiredLogin" runat="server" ControlToValidate="TextBoxLogin"
		ErrorMessage="Введите логин." Display="None" ValidationGroup="ValidateAuthData" />
	<asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="TextBoxPass"
		ErrorMessage="Введите пароль." Display="None" ValidationGroup="ValidateAuthData" />
	<asp:CustomValidator ID="WrongAccount" runat="server" Display="None"
		ErrorMessage="Неправильный логин/пароль." ValidationGroup="ValidateAuthData" />
	<asp:CustomValidator ID="UserIsBanned" runat="server" Display="None"
		ErrorMessage="Ваш аккаунт забанен. Вы не можете авторизоваться" ValidationGroup="ValidateAuthData" />
         */
        private List<string> ValidationMessages() 
        {
            List<string> errors = new List<string>();
            if (TextBoxLogin.Text == "") {
                errors.Add("Введите логин");
            }
            if (TextBoxPass.Text == "") {
                errors.Add("Введите пароль");
            }
            return errors;
        }

        //TODO: хммм
        protected void LogInButton_Click(object sender, EventArgs e)
        {
            string login = TextBoxLogin.Text;
            string pass = TextBoxPass.Text;
            bool remember = CheckBoxIsRemember.Checked;

            List<string> messages = ValidationMessages();
            if (messages.Count == 0)
            {
                if (CurrentUser.LogIn(login, pass, remember)) {
                    if (CurrentUser.User.Role == User.Roles.Banned) {
                        messages.Add("Ваш аккаунт забанен. Вы не можете авторизоваться");
                        CurrentUser.LogOut();
                    } else {
                        //Response.Redirect(FormsAuthentication.GetRedirectUrl(login, false));
                        string targetUrl = Request.Params["ReturnUrl"] == null ? "default.aspx" : Request.Params["ReturnUrl"];
                        Response.Redirect(targetUrl);
                    }
                } else {
                    messages.Add("Неправильный логин/пароль");
                }
            }
            WriteMessages(messages);
        }

        private void WriteMessages(List<string> errors) {
            string text = "<div class=\"error\"><ul>";
            foreach (string message in errors) {
                text += "<li>" + message + "</li>";
            }
            text += "</ul></div>";
            Messages.Text = text;
        }
    }
}