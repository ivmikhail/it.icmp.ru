<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="ITCommunity.ProfilePage" Title="Ykt IT Community | Редактирование профиля" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Ваш профиль</h1>

	<label class="textbox-input">
		<h3>Изменить email</h3>
		<p class="note"> 
			Не обязательно
		</p>
		<asp:TextBox ID="TextBoxEmail" runat="server" MaxLength="512" ValidationGroup="ValidateProfileData"/>
	</label>

	<label class="textbox-input">
		<h3>Новый пароль</h3> 
		<p class="note">
			Не обязательно, если ничего не введете, то пароль не изменится
		</p>
		<asp:TextBox ID="TextBoxNewPass" runat="server" TextMode="Password" MaxLength="512" ValidationGroup="ValidateProfileData"/>
	</label>

	<label class="textbox-input">
		<h3>Повторите новый пароль</h3>
		<asp:TextBox ID="TextBoxNewPassConf" runat="server" TextMode="Password" MaxLength="512" ValidationGroup="ValidateProfileData"/>
	</label>

	<label class="textbox-input">
		<h3>Введите ваш текущий пароль для продолжения</h3>
		<p class="note">
			Чтобы сохранить изменения введите ваш текущий пароль
		</p>
		<asp:TextBox ID="TextBoxPassConf" runat="server" TextMode="Password" MaxLength="512"/>
	</label>

	<asp:Literal ID="LiteralUpdatedMessage" runat="server" />

	<asp:ValidationSummary ID="ValidationSummaryProfile" runat="server" ValidationGroup="ValidateProfileData" DisplayMode="List" CssClass="error" />
	<asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="TextBoxEmail"
		ErrorMessage="Введите e-mail." ValidationGroup="ValidateProfileData" Display="None" />
	<asp:RegularExpressionValidator ID="EmailValidator" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Введите нормальный e-mail."
		Display="None" ValidationGroup="ValidateProfileData" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
	<asp:CustomValidator ID="EmailExist" runat="server" Display="None"
		ErrorMessage="Пользователь с таким email'ом уже зарегистрирован. Изменения не вступили в силу." ValidationGroup="ValidateProfileData" />
	<asp:CustomValidator ID="OldPassConfirm" runat="server" Display="None"
		ErrorMessage="Старый пароль не верен, изменения не вступили в силу." ValidationGroup="ValidateProfileData" />
	<asp:CompareValidator ID="ConfirmPassword" runat="server" Display="None" ControlToCompare="TextBoxNewPass"
		ControlToValidate="TextBoxNewPassConf" ErrorMessage="Пароли не совпадают." ValidationGroup="ValidateProfileData" />

	<div class="big-button">
			<asp:LinkButton ID="EditProfileButton" runat="server" OnClick="EditProfileButton_Click" ValidationGroup="ValidateProfileData">Изменить</asp:LinkButton>
	</div>

</asp:Content>
