<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentsList.ascx.cs" Inherits="ITCommunity.CommentsList" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

	<script type="text/javascript">
	    <!--
	    var newTextHiddenId = '<%= NewTextHidden.ClientID %>';
	    function setCommentText() {
	        $$(".newCommentText").each(function (el) {            
		         $(newTextHiddenId).value = el.value;
	        });
        }
		// -->
	</script>
    
	<asp:HiddenField ID="NewTextHidden" runat="server" />
<asp:Repeater ID="RepeaterComments" runat="server" OnItemCommand="RepeaterComments_ItemCommand" OnItemDataBound="RepeaterComments_ItemDataBound">
	<HeaderTemplate>		
        <ul>
	</HeaderTemplate>    
	<ItemTemplate>
		<li id="comment-<%# Eval("id")%>" class="even">
			<div class="comment-info">
				<a href="user.aspx?login=<%# Eval("author.login")%>" title="Посетить страницу пользователя" class="user-pm-link"><%# Eval("author.login")%></a>
				- <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%>
				<a href="news.aspx?id=<%# Eval("postid")%>#comment-<%# Eval("id")%>" title="Постоянная ссылка на комментарий">#</a>
		     	<asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="UpdateComment" runat="server" Text="обновить"      CommandName="update" OnClientClick="setCommentText();" />
		        <asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="EditComment"   runat="server" Text="редактировать" CommandName="edit" >редактировать</asp:LinkButton>
                <asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="DeleteComment" runat="server" Text="удалить"       CommandName="delete" OnClientClick="return confirm('Точно удалить?')" />
			    <asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="CancelEdit"    runat="server" Text="отменить"      CommandName="cancel"  />
		
                <asp:Literal ID="EditError" runat="server" Visible="true"/>
            </div>
			<div class="comment-text">
                <asp:Literal ID="CommentText" runat="server" />
                <asp:TextBox ID="NewCommentText" runat="server" Visible="false" TextMode="MultiLine" Rows="10" Width="100%" CssClass="newCommentText"/>
			</div>
		</li>
	</ItemTemplate>
	<AlternatingItemTemplate>
		<li id="comment-<%# Eval("id")%>" class="odd">
			<div class="comment-info">
				<a href="user.aspx?login=<%# Eval("author.login")%>" title="Посетить страницу пользователя" class="user-pm-link"><%# Eval("author.login")%></a>
				- <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%>
				<a href="news.aspx?id=<%# Eval("postid")%>#comment-<%# Eval("id")%>" title="Постоянная ссылка на комментарий">#</a>
				<asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="UpdateComment" runat="server" Text="обновить" CommandName="update"  OnClientClick="setCommentText();"/>
		        <asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="EditComment"   runat="server" Text="редактировать" CommandName="edit" >редактировать</asp:LinkButton>
                <asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="DeleteComment" runat="server" Text="удалить" CommandName="delete" OnClientClick="return confirm('Точно удалить?')" />
				<asp:LinkButton Visible="false" CommandArgument='<%# Eval("id") + "," + Eval("postid")%>' ID="CancelEdit"    runat="server" Text="отменить" CommandName="cancel"  />
                
                <asp:Literal ID="EditError" runat="server" Visible="true" />
            </div>
			<div class="comment-text">
				<asp:Literal ID="CommentText" runat="server" />            
                <asp:TextBox ID="NewCommentText" runat="server" Visible="false" TextMode="MultiLine" Rows="10" Width="100%" CssClass="newCommentText"/>
			</div>
		</li>
	</AlternatingItemTemplate>
	<FooterTemplate>
		</ul>
	</FooterTemplate>
</asp:Repeater>
