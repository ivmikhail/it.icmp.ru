<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="whoami.aspx.cs" Inherits="ITCommunity.WhoamiPage" Title="Ykt IT Community | Кто я?" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<h1>HTTP заголовки</h1>
	<div class="note">
		На данной странице представлена информация о вас, отправляемая вашим браузером с запросом.
	</div>

	<asp:Repeater ID="RepeaterData" runat="server">
			<HeaderTemplate>
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
				<li>
					<h3><%# Eval("key")%></h3>
					<p><%# Eval("value")%></p>
				</li>
			</ItemTemplate>
			<FooterTemplate>
				</ul>
			</FooterTemplate>
	</asp:Repeater>

</asp:Content>
