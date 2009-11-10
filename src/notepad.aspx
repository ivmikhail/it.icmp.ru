<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notepad.aspx.cs" Inherits="ITCommunity.Notepad" Title="Ykt IT Community | Блокнот" %>

<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Блокнот <a href='notepadadd.aspx'>добавить запись</a></h1>

	<asp:Repeater ID="RepeaterNotes" runat="server">
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<h2><%# Eval("title")%></h2>
				<p><%# Eval("text")%></p>
				<div class="info-panel">
					<%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%> / <a href='notepad.aspx?del=<%# Eval("id")%>'>Удалить</a>
				</div>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>

	<uc:Pager id="NotesPager" runat="server" />
</asp:Content>
