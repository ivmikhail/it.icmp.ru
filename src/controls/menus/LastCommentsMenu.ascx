<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LastCommentsMenu.ascx.cs" Inherits="ITCommunity.LastCommentsMenu" %>

<div id="last-comments-menu" class="menu-panel">
	<h1>Последние комментарии</h1>

	<asp:Repeater ID="LastComments" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="user.aspx?login=<%# Eval("key.login")%>" title="Посетить страницу пользователя" class="user-pm-link">
					<%# Eval("key.login")%>
				</a>
				:
				<a href='news.aspx?id=<%# Eval("value.postid")%>#comment-<%# Eval("value.id")%>' title="<%# Eval("value.post.title")%>" >
					<%# Eval("value.shorttext")%>
				</a>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
