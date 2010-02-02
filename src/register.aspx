<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="ITCommunity.Register" Title="Ykt It Community | Регистрация" %>

<%@ Register Src="~/controls/ItCaptcha.ascx" TagName="ItCaptcha" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:Panel ID="RegisterPanel" runat="server">
		<h1>Регистрация</h1>
		<div class="note">
			<h2>После регистрации Вас ждут следующие вкусности</h2>
			<ul class="list">
				<li>Электронные книги</li>
				<li>Доступ к видео курсам</li>
				<li>Доступ к различным файлам</li>
				<li>Возможность поиска по сайту</li>
				<li>Вы можете опубликовать свою новость</li>
				<li>доступ к различным сервисам</li>
			</ul>
		</div>

		<label class="textbox-input">
			<span class="label-title">Логин (аккаунт)</span>
			<span class="note">RegExp паттерн валидного логина: ^[A-Za-z0-9_\-\.]{2,20}$ </span>
			<asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateRegData" MaxLength="32"/>
		</label>

		<label class="textbox-input">
			<span class="label-title">Электропочта</span>
			<span class="note">Нигде не публикуется, используется для восстановления пароля</span>
			<asp:TextBox ID="TextBoxEmail" runat="server" ValidationGroup="ValidateRegData" CssClass="input-text" MaxLength="512"/>
		</label>

		<label class="textbox-input">
			<span class="label-title">Пароль</span>
			<asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateRegData" TextMode="Password" CssClass="input-text" MaxLength="512"/>
		</label>

		<label class="textbox-input">
			<span class="label-title">Повторите пароль</span>
			<asp:TextBox ID="TextBoxPassConf" runat="server" ValidationGroup="ValidateRegData" TextMode="Password" CssClass="input-text" MaxLength="512"/>
		</label>

		<uc:ItCaptcha ID="captcha" runat="server" Visible="true" EnableViewState="true"/>

		<asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateRegData" DisplayMode="List" CssClass="error" />

		<asp:RequiredFieldValidator ID="RequiredLogin" runat="server" ControlToValidate="TextBoxLogin"
			ErrorMessage="Введите логин" Display="None" ValidationGroup="ValidateRegData" />
		<asp:RegularExpressionValidator ID="LoginValidator" runat="server" ControlToValidate="TextBoxLogin"
			ErrorMessage="Логин может состоять только из латинских букв, цифр, знаков '-','.' и '_'. Длина должна быть от 3-х до 25-и символов."
			Display="None" ValidationGroup="ValidateRegData" ValidationExpression="^[A-Za-z0-9_\-\.]{2,20}$" />
		<asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="TextBoxEmail"
			ErrorMessage="Введите e-mail" Display="None" ValidationGroup="ValidateRegData" />
		<asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="TextBoxEmail"
			ErrorMessage="Введите нормальный e-mail." Display="None" ValidationGroup="ValidateRegData" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"/>
		<asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="TextBoxPass"
			ErrorMessage="Введите пароль" Display="None" ValidationGroup="ValidateRegData" />
		<asp:CompareValidator ID="ConfirmPassword" runat="server" Display="None"
			ControlToCompare="TextBoxPass" ControlToValidate="TextBoxPassConf" ErrorMessage="Пароли не совпадают" ValidationGroup="ValidateRegData" />
		<asp:CustomValidator ID="AccountExist" runat="server" Display="None"
			ErrorMessage="Пользователь с таким логином/email'ом уже зарегистрирован" ValidationGroup="ValidateRegData" />
		<asp:CustomValidator ID="AnonymousAccount" runat="server" Display="None"
			ErrorMessage="Нельзя регистрировать аккаунт с логином 'anonymous', выберите другой логин" ValidationGroup="ValidateRegData" />
		<asp:CustomValidator ID="RegisterFailed" runat="server" Display="None"
			ErrorMessage="Ошибка, регистрация не прошла, обратитесь к администратору" ValidationGroup="ValidateRegData" />

		<div class="big-button">
			<asp:LinkButton ID="RegisterButton" runat="server" ValidationGroup="ValidateRegData" OnClick="RegisterButton_Click">Зарегистрируйте меня, я готов!</asp:LinkButton>
		</div>

	</asp:Panel>

	<asp:Literal ID="YetRegisteredText" runat="server" Visible="false">
		<h1 class="message">Вы уже зарегистрированы и авторизовались</h1>
		<div class="note">Если вы все-таки хотите зарегистрироватсья по новой, тогда разлогинтесь</div>
	</asp:Literal>

</asp:Content>

