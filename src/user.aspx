<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="ITCommunity.UserPage" Title="Страница пользователя" %>

<%@ Register Src="~/controls/PostsList.ascx"        TagName="PostsList"        TagPrefix="uc" %>
<%@ Register Src="~/controls/UserCommentsList.ascx" TagName="UserCommentsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Страница пользователя</h1>
    <asp:Literal ID="UserNotFound" runat="server" Text="<div class='error'>Пользователь не найден</div>" />
    <div id="UserPanel" runat="server">
        <b><asp:Literal ID="UserLogin" runat="server" /></b>, дата регистрации - <asp:Literal ID="RegDate" runat="server" />

        <br />
        <br />    
        <asp:HyperLink ID="PostsLink" runat="server">Посты</asp:HyperLink> |
        <asp:HyperLink ID="CommentsLink" runat="server">Комментарии</asp:HyperLink> |
        <asp:HyperLink ID="SendMessageLink" runat="server">Отправить сообщение</asp:HyperLink>
    
        <h1><asp:Literal ID="PageInfo" runat="server" /></h1>
        <uc:PostsList    ID="UserPostsList" runat="server" />
        <uc:UserCommentsList ID="UserCommentsList" runat="server" />    

    </div>

</asp:Content>

