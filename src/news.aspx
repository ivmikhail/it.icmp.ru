<%@ Page Language="C#" EnableViewState="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="ITCommunity.News" Title="Ykt It Community | " %>

<%@ Register src="~/controls/ItCaptcha.ascx"       tagname="ItCaptcha"     tagprefix="uc" %>
<%@ Register src="~/controls/BBCodeInfo.ascx"      tagname="BBCodeInfo"    tagprefix="uc" %>
<%@ Register src="~/controls/EditorToolbar.ascx"   tagname="EditorToolbar" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
	<div class="post"> 
		<h1>
			<asp:HyperLink ID="HyperLinkTitle" runat="server" CssClass="title-link">HyperLink</asp:HyperLink>
			(<asp:Literal ID="LinksPostCategories" runat="server"></asp:Literal>)
		</h1>

		<div class="post-desc">
			<asp:Literal ID="desc" runat="server" />
		</div>
        <asp:Literal ID="DescSeparator" runat="server" />
		<div id="cut" class="post-text">
			<asp:Literal ID="text" runat="server" />  
		</div>

		<div class="post-info">
			<asp:Literal ID="date" runat="server" /> /
			<asp:Literal ID="favorite" runat="server" /> /
			<a href='mailsend.aspx?receiver=<asp:Literal ID="author" runat="server" />' title="Отправить личное сообщение" class="user-pm-link"><asp:Literal ID="authorLogin" runat="server" /></a> /
			просмотров: <asp:Literal ID="views" runat="server" />
			<asp:Literal ID="EditPostLink" runat="server" Visible="false" />
			<asp:LinkButton ID="DeletePostLink" runat="server" OnClick="DeletePost_Click" Visible="false" OnClientClick="return confirm('Точно удалить?')">удалить</asp:LinkButton>
			<asp:Literal ID="source" runat="server" />
		</div>

		<div id="comments" class="panel">
			<h2>Комментарии (<asp:Literal ID="comments_count" runat="server" />)</h2>
			<asp:Repeater ID="RepeaterComments" runat="server" OnItemCommand="RepeaterComments_ItemCommand" OnItemDataBound="RepeaterComments_ItemDataBound">
				<HeaderTemplate>
					<ul>
				</HeaderTemplate>
				<ItemTemplate>
					<li id="comment-<%# Eval("id")%>" class="even">
						<div class="comment-info">
							<a href="mailsend.aspx?receiver=<%# Eval("author.nick")%>" title="Отправить личное сообщение" class="user-pm-link"><%# Eval("author.nick")%></a>
							- <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%>
							<asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") %>' ID="DeleteComment" runat="server" Text="Удалить" CommandName="delete" OnClientClick="return confirm('Точно удалить?')" />
						</div>
						<div class="comment-text">
							<%# Eval("textformatted")%>
						</div>
					<li>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<li id="comment-<%# Eval("id")%>" class="odd">
						<div class="comment-info">
							<a href="mailsend.aspx?receiver=<%# Eval("author.nick")%>" title="Отправить личное сообщение" class="user-pm-link"><%# Eval("author.nick")%></a>
							- <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%>
							<asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") %>' ID="DeleteComment" runat="server" Text="Удалить" CommandName="delete" OnClientClick="return confirm('Точно удалить?')" />
						</div>
						<div class="comment-text">
							<%# Eval("textformatted")%>
						</div>
					<li>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</ul>
				</FooterTemplate>
			</asp:Repeater>
		 </div>

		<div id="add-comment-panel" class="panel">
			<h2>Написать комментарий. Вы - <asp:Literal ID="userLogin" runat="server" /></h2>

			<uc:BBCodeInfo ID="BBCodeInfo" runat="server"/>

			<uc:ItCaptcha ID="Captcha" runat="server" Visible="false" EnableViewState="true"/>

            <uc:EditorToolbar ID="EditorToolbar" runat="server" ToolbarElements="" />
			<label class="textbox-textarea">
				<asp:TextBox ID="TextBoxComment" runat="server" TextMode="MultiLine" Rows="10" MaxLength="512" ValidationGroup="Comment" />
			</label>

			<asp:RequiredFieldValidator ID="RequiredFieldValidatorComment" runat="server" ErrorMessage="Введите комментарий" ControlToValidate="TextBoxComment" ValidationGroup="Comment" />

			<div class="big-button">
				<asp:LinkButton ID="LinkButtonAddComment" runat="server" OnClick="LinkButtonAddComment_Click" ValidationGroup="Comment">Добавить</asp:LinkButton>
			</div>
		</div>
	</div>
</asp:Content>
