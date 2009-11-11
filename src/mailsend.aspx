<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mailsend.aspx.cs" Inherits="ITCommunity.Mailsend" Title="Ykt IT Community | Отправка сообщения" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<a href='mailsend.aspx'>Написать</a> |
	<a href='mailview.aspx'>Входящие</a> |
	<a href='mailview.aspx?a=output'>Исходящие</a>

	<h1>Отправка сообщения</h1>

	<label class="textbox-input">
		<span class="label-title">Кому</span>
		<asp:TextBox ID="MessageReceiver" runat="server" MaxLength="20" />
	</label>

	<label class="textbox-input">
		<span class="label-title">Заголовок</span>
		<asp:TextBox ID="MessageTitle" runat="server" MaxLength="50" />
	</label>

	<label class="textbox-textarea">
		<span class="label-title">Текст</span>
		<asp:TextBox ID="MessageText" runat="server" Rows="10" MaxLength="1000" TextMode="MultiLine"/>
	</label>

	<asp:Literal ID="Errors" runat="server" />

	<div class="big-button">
		<a href='mailview.aspx?'>Отмена</a>
		<asp:LinkButton ID="LinkButtonSend" runat="server" OnClick="LinkButtonSend_Click">Отправить</asp:LinkButton>
	</div>
</asp:Content>
