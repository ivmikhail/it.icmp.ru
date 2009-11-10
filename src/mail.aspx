<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mail.aspx.cs" Inherits="ITCommunity.Mail" Title="Ykt IT Community | Просмотр сообщения" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<a href='mailsend.aspx'>Написать</a> |
	<a href='mailview.aspx'>Входящие</a> |
	<a href='mailview.aspx?a=output'>Исходящие</a>

	<h1>Просмотр сообщения</h1>
	<ul>
		<li>
			<asp:Literal ID="LiteralWho" runat="server" />
		</li>
		<li>
			<h3>Заголовок</h3>
			<asp:Literal ID="MessageTitle" runat="server" />
		</li>
		<li>
			<h3>Текст</h3> 
			<asp:Literal ID="MessageText" runat="server" />
		</li>
		<li class="big-button">
			<asp:LinkButton ID="DeleteLink" runat="server" OnClick="DeleteLink_Click">Удалить</asp:LinkButton>
			<asp:Literal ID="ReplyLink" runat="server" />
			<asp:LinkButton ID="LinkButtonBack" runat="server" OnClick="LinkButtonBack_Click">Назад к списку</asp:LinkButton>
		</li>
	</ul>
</asp:Content>
