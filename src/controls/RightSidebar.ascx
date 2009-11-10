<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightSidebar.ascx.cs" Inherits="ITCommunity.RightSidebar" %>

<%@ Register Src="~/controls/menus/LastCommentsMenu.ascx" TagName="LastCommentsMenu" TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/PollMenu.ascx"         TagName="PollMenu"         TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/PopularPostsMenu.ascx" TagName="PopularPostsMenu" TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/SearchMenu.ascx"       TagName="SearchMenu"       TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/TopPostersMenu.ascx"   TagName="TopPostersMenu"   TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/UsersStatsMenu.ascx"   TagName="UsersStatsMenu"   TagPrefix="uc" %>

<div id="right-sidebar">
	<div class="content">

		<uc:SearchMenu ID="SearchMenu" runat="server" />

		<uc:PollMenu ID="PollMenu" runat="server" />

		<uc:PopularPostsMenu ID="PopularPostsMenu" runat="server" />

		<uc:LastCommentsMenu ID="LastCommentsMenu" runat="server" />

		<uc:TopPostersMenu ID="TopPostersMenu" runat="server" />

		<uc:UsersStatsMenu ID="UsersStatsMenu" runat="server" />

	</div>
</div>
