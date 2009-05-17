<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PostManage.ascx.cs" Inherits="ITCommunity.PostManage" %>
<div ID="PostManager" runat="server" class="post-manage" visible="false">
    <asp:linkbutton runat="server" id="DeletePost" OnClick="DeletePost_Click" >удалить</asp:linkbutton> /    
    <asp:Literal ID="EditPostLink" runat="server"/>
</div>