<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsersStatsMenu.ascx.cs" Inherits="ITCommunity.UsersStatsMenu" %>

<div id="users-stats-menu" class="menu-panel">
	<h1>Пользователи</h1>

	<h3>Новые</h3>
	<asp:Repeater ID="LastRegistered" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<%# Eval("createdate", "{0:dd MMMM}")%>  <a href="mailsend.aspx?receiver=<%# Eval("nick") %>" title="Отправить личное сообщение" class="user-pm-link"><%# Eval("nick")%></a>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>

	<h3>Количество</h3>
	<ul>
		<li>
			Всего - <asp:Literal ID="TotalUsers" runat="server" />
		</li>
		<li>
			Постеры -  <asp:Literal ID="TotalPosters" runat="server" />
		</li>
		<li>
			Админы - <asp:Literal ID="TotalAdmins" runat="server" />
		</li>
	</ul>
</div>