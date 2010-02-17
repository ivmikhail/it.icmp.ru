<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PopularPostsMenu.ascx.cs" Inherits="ITCommunity.PopularPostsMenu" %>

<div id="popular-posts-menu" class="menu-panel">
	<h1>Популярные посты</h1>

    <h3>По просмотрам</h3>
	<asp:Repeater ID="PopularPostsByViews" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="news.aspx?id=<%# Eval("value.id")%>">
					<%# Eval("value.TitleFormatted")%>
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
    <%-- 
    <h3>По рейтингу</h3>
    <asp:Repeater ID="PopularPostsByRating" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="news.aspx?id=<%# Eval("value.id")%>">
					<%# Eval("value.TitleFormatted")%>
				</a>
				by
				<a href="user.aspx?login=<%# Eval("key.login")%>" title="Посетить страницу пользователя" class="user-pm-link">
					<%# Eval("key.login")%>
				</a>
                <div style="float:right;">
				    <uc:Rating ID="PostRating" runat="server" EntityId='<%# Eval("value.id") %>' Type="Post" EntityAuthorId='<%# Eval("value.authorId") %>' ButtonsVisible="false" />
			    </div>
            </li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
    --%>    
</div>
