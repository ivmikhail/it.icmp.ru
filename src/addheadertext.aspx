<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addheadertext.aspx.cs" Inherits="ITCommunity.AddHeaderText" Title="Ykt IT Community | Добавление текста хидера" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Добавление текста в хидере</h1>
	<div class="note">
		Вы можете написать любой текст длиной до <%=HeaderTextMaxLength %> символов, который будет появляться наверху (в header) сайта. 
		Тексты появляются в случайном порядке.
		Текст будет жить <%= HeaderTextShowingHours %> часов после добавления.
		Запрещена любая реклама, нецензурщина и оскорбления. Несоответствующие требованиям тексты будут удаляться.
	</div>

	<label class="textbox-input">
		<span class="label-title">Текст</span>
		<asp:TextBox ID="TextBoxText" runat="server" />
	</label>
	
	<asp:Literal ID="MessageText" runat="server" Visible="false">
		<div class="message">
			Ваш текст добавлен.
		</div>
	</asp:Literal>

	<asp:Panel ID="ErrorPanel" runat="server" Visible="false">
		<div class="error">
			Вы не можете добавить текст для хидера. Вам нужно написать еще <%= PostsCount %>) постов.
		</div>
	</asp:Panel>

	<asp:Panel ID="UserBlockedErrorPanel" runat="server" Visible="false">
		<div class="error">
			Вы не можете добавить текст для хидера, так как мы считаем, что вы плохо вели себя.
		</div>
	</asp:Panel>

	<asp:Panel ID="TextLengthError" runat="server" Visible="false">
		<div class="error">
			Длина текста может быть только до <%= HeaderTextMaxLength %> символов.
		</div>
	</asp:Panel>

	<asp:ValidationSummary ID="ValidationSummaryAddpoll" runat="server" ValidationGroup="ValidateHeaderText" DisplayMode="List" CssClass="error" />
	<asp:RequiredFieldValidator ID="RequiredText" runat="server" ControlToValidate="TextBoxText"
		ErrorMessage="Введите текст хидера." Display="None" ValidationGroup="ValidateHeaderText" />

	<div class="big-button">
		<asp:LinkButton ID="LinkButtonAddHeaderText" runat="server" OnClick="LinkButtonAddHeaderText_Click">добавить</asp:LinkButton>
	</div>
</asp:Content>
