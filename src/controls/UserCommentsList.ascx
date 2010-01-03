<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserCommentsList.ascx.cs" Inherits="ITCommunity.UserCommentsList" %>

<%@ Register Src="~/controls/Pager.ascx"        TagName="Pager"         TagPrefix="uc" %>
<%@ Register src="~/controls/CommentsList.ascx" TagName="CommentsList"  TagPrefix="uc" %>

<div id="comments" class="panel">
    <uc:CommentsList ID="CommentsList" runat="server" />
</div>
<uc:Pager id="Pager" runat="server" />
