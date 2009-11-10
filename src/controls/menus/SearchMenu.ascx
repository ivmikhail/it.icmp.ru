<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchMenu.ascx.cs" Inherits="ITCommunity.SearchMenu" %>

<div id="search-panel" class="menu-panel">
	<h1>Поиск</h1>

	<asp:TextBox ID="TextBoxQuery" runat="server" />
	<asp:LinkButton ID="LinkButtonSearch" runat="server" OnClick="LinkButtonSearch_Click">Найти</asp:LinkButton>
</div>
