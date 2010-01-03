<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopularPostsMenu.ascx.cs" Inherits="ITCommunity.PopularPostsMenu" %>

<div id="popular-posts-menu" class="menu-panel">
	<h1>Популярные посты</h1>

	<asp:Repeater ID="PopularPosts" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href='news.aspx?id=<%# Eval("value.id")%>'>
					<%# Eval("value.title")%>
				</a>
				by
				<a href="user.aspx?login=<%# Eval("key.login")%>" title="Посетить страницу пользователя" class="user-pm-link">
					<%# Eval("key.login")%>
				</a>
				(<%# Eval("value.views")%>)
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
