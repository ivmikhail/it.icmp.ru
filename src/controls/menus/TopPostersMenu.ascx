<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopPostersMenu.ascx.cs" Inherits="ITCommunity.TopPostersMenu" %>

<div id="top-posters-menu" class="menu-panel">
	<h1>Активные постеры</h1>
	<h3>За последние <asp:Literal ID="LastTopPostersDays" runat="server" /> дней</h3>
	<asp:Repeater ID="LastTopPosters" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="user.aspx?login=<%# Eval("key") %>" title="Посетить страницу пользователя" class="user-pm-link"><%# Eval("key")%></a> (<%# Eval("value")%>)
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
	<h3>За все время</h3>
	<asp:Repeater ID="TopPosters" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="user.aspx?login=<%# Eval("key") %>" title="Посетить страницу пользователя" class="user-pm-link"><%# Eval("key")%></a> (<%# Eval("value")%>)
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
