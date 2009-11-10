<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftSidebar.ascx.cs" Inherits="ITCommunity.LeftSidebar" %>

<%@ Register Src="~/controls/menus/AdminMenu.ascx"       TagName="AdminMenu"      TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/CategoriesMenu.ascx"  TagName="CategoriesMenu" TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/LinksMenu.ascx"       TagName="LinksMenu"      TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/LoginMenu.ascx"       TagName="LoginMenu"      TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/RedmineActivity.ascx" TagName="Redmine"        TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/UserMenu.ascx"        TagName="UserMenu"       TagPrefix="uc" %>

<div id="left-sidebar">
	<div class="content">

		<uc:LoginMenu id="LoginMenu" runat="server" />

		<uc:UserMenu id="UserMenu" runat="server" />

		<uc:AdminMenu id="AdminMenu" runat="server"/>

		<uc:CategoriesMenu id="CategoriesMenu" runat="server"/>

		<uc:LinksMenu id="LinksMenu" runat="server"/>

		<uc:Redmine id="Redmine" runat="server" />

	</div>
</div>
