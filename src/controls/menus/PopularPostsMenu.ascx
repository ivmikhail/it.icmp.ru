<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopularPostsMenu.ascx.cs" Inherits="ITCommunity.PopularPostsMenu" %>

<div id="popular-posts-menu" class="menu-panel">
	<h1>Популярные посты</h1>

	<h3>По просмотрам</h3>
	<asp:Repeater ID="PopularPostsByViews" runat="server" >
		<HeaderTemplate>
			<ul class="by-views">
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<%# Eval("Views")%>
				<a href="news.aspx?id=<%# Eval("Id")%>">
					<%# Eval("TitleFormatted")%>
				</a>
				by
				<a href="user.aspx?login=<%# Eval("Author.Login")%>" title="Посетить страницу пользователя" class="user-pm-link">
					<%# Eval("Author.Login")%>
				</a>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>

	<h3>По рейтингу</h3>
	<asp:Repeater ID="PopularPostsByRating" runat="server" >
		<HeaderTemplate>
			<ul class="by-rating">
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<%# Eval("Value")%>
				<a href="news.aspx?id=<%# Eval("Key.Id")%>">
					<%# Eval("Key.TitleFormatted")%>
				</a>
				by
				<a href="user.aspx?login=<%# Eval("Key.Author.Login")%>" title="Посетить страницу пользователя" class="user-pm-link">
					<%# Eval("Key.Author.Login")%>
				</a>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
