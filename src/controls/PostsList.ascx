<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PostsList.ascx.cs" Inherits="ITCommunity.PostsList" %>

<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>

<asp:Repeater ID="RepeaterPosts" runat="server" OnItemDataBound="RepeaterPosts_ItemDataBound">
	<HeaderTemplate>
		<ul id="posts-list">
	</HeaderTemplate>
	<ItemTemplate>
		<li class="post">
			<h1>
				<asp:Image ID="AttachedImage" runat="server" ImageUrl="../media/img/design/attached.jpg" Visible="false" CssClass="attached-image" AlternateText="Важная новость" />
				<a href='news.aspx?id=<%# Eval("id")%>' title="Посмотреть полный текст" class="title-link"><%# Eval("title")%></a>
				<asp:Repeater ID="RepeaterPostCategories" runat="server">
					<HeaderTemplate>(</HeaderTemplate>
					<ItemTemplate><a href='default.aspx?cat=<%# Eval("id")%>' title="Посмотреть новости этой категории" class="category-link"><%# Eval("name")%></a></ItemTemplate>
					<FooterTemplate>)</FooterTemplate>
					<SeparatorTemplate>, </SeparatorTemplate>
				</asp:Repeater>
			</h1>

			<div class="post-desc">
				<%# (Eval("descriptionformatted").ToString() == "") ? Eval("textformatted") : Eval("descriptionformatted") %>
			</div>

			<div class="post-info">
				<%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%> /
				<%# Eval("favoritesaction") %> /
				<a href="mailsend.aspx?receiver=<%# Eval("author.nick")%>" title="Отправить личное сообщение автору" class="user-pm-link"><%# Eval("author.nick")%></a> /
				просмотров: <%# Eval("views")%> /
				<a href='news.aspx?id=<%# Eval("id")%>#comments' title="Посмотреть комментарии" class="post-comments-link">комментарии(<%# Eval("commentscount")%>)</a> /
				<a href='news.aspx?id=<%# Eval("id")%>#cut' title="Читать далее">подробнее...</a>
			</div>
		</li>
	</ItemTemplate>
	<FooterTemplate>
		</ul>
	</FooterTemplate>
</asp:Repeater>

<uc:Pager id="Pager" runat="server" />
