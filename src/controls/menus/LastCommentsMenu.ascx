<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LastCommentsMenu.ascx.cs" Inherits="ITCommunity.LastCommentsMenu" %>

<div id="last-comments-menu" class="menu-panel">
	<h1>Последние комментарии</h1>

	<asp:Repeater ID="LastComments" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="mailsend.aspx?receiver=<%# Eval("key.nick")%>" title="Отправить личное сообщение автору" class="user-pm-link">
					<%# Eval("key.nick")%>
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
