<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginMenu.ascx.cs" Inherits="ITCommunity.LoginMenu" %>

<div id="login-menu" class="menu-panel">
	<h1>Авторизация</h1>

	<label class="textbox-label">
		Логин
		<asp:TextBox ID="TextBoxLogin" runat="server" />
	</label>

	<label class="textbox-label">
		Пароль
		<asp:TextBox ID="TextBoxPass" runat="server" TextMode="Password" />
	</label>

	<label class="checkbox-label">
		Запомнить
		<asp:CheckBox ID="CheckBoxIsRemember" runat="server" />
	</label>
	
	<ul>
		<li>
			<asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click"  ToolTip="Войти в аккаунт">Вход</asp:LinkButton>
			<a href="register.aspx" title="Присоединиться к этому чудесному сообществу">Регистрация</a>
		</li>
		<li>
			<a href="recovery.aspx" title="Жми сюда если забыл пароль">Я забыл пароль</a>
		</li>
	</ul>
	<asp:Literal ID="Messages" runat="server" />
</div>
