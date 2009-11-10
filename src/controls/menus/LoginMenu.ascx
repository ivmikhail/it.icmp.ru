<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginMenu.ascx.cs" Inherits="ITCommunity.LoginMenu" %>

<div id="login-menu" class="menu-panel">
	<h1>Авторизация</h1>

	<label class="textbox-label">
		Логин
		<asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateAuthData" />
	</label>

	<label class="textbox-label">
		Пароль
		<asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateAuthData" TextMode="Password" />
	</label>

	<label class="checkbox-label">
		Запомнить
		<asp:CheckBox ID="CheckBoxIsRemember" runat="server" />
	</label>
	
	<ul>
		<li>
			<asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click" ValidationGroup="ValidateAuthData" ToolTip="Войти в аккаунт">Вход</asp:LinkButton>
			<a href="register.aspx" title="Присоединиться к этому чудесному сообществу">Регистрация</a>	
		</li>
		<li>
			<a href="recovery.aspx" title="Жми сюда если забыл пароль">Я забыл пароль</a>
		</li>
	</ul>

	<asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateAuthData" DisplayMode="List" CssClass="error" />

	<asp:RequiredFieldValidator ID="RequiredLogin" runat="server" ControlToValidate="TextBoxLogin"
		ErrorMessage="Введите логин." Display="None" ValidationGroup="ValidateAuthData" />
	<asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="TextBoxPass"
		ErrorMessage="Введите пароль." Display="None" ValidationGroup="ValidateAuthData" />
	<asp:CustomValidator ID="WrongAccount" runat="server" Display="None"
		ErrorMessage="Неправильный логин/пароль." ValidationGroup="ValidateAuthData" />
	<asp:CustomValidator ID="UserIsBanned" runat="server" Display="None"
		ErrorMessage="Ваш аккаунт забанен. Вы не можете авторизоваться" ValidationGroup="ValidateAuthData" />
</div>
