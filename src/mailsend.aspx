<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mailsend.aspx.cs" Inherits="ITCommunity.Mailsend" Title="Ykt IT Community | Отправка сообщения" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<a href='mailsend.aspx'>Написать</a> |
	<a href='mailview.aspx'>Входящие</a> |
	<a href='mailview.aspx?a=output'>Исходящие</a>

	<h1>Отправка сообщения</h1>

	<asp:Literal ID="Errors" runat="server" />

	<label class="textbox-input">
		<h3>Кому</h3>
		<asp:TextBox ID="MessageReceiver" runat="server" MaxLength="20" />
	</label>

	<label class="textbox-input">
		<h3>Заголовок</h3>
		<asp:TextBox ID="MessageTitle" runat="server" MaxLength="50" />
	</label>

	<label class="textbox-textarea">
		<h3>Текст</h3>
		<asp:TextBox ID="MessageText" runat="server" Rows="15" MaxLength="1000" TextMode="MultiLine"/>
	</label>

	<div class="big-button">
		<a href='mailview.aspx?'>Отмена</a>
		<asp:LinkButton ID="LinkButtonSend" runat="server" OnClick="LinkButtonSend_Click">Отправить</asp:LinkButton>
	</div>
</asp:Content>
