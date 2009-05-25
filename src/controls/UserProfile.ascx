<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="ITCommunity.UserProfile" %>
<div id="user_profile">
<h2>Aloha <asp:Literal ID="LabelUserLogin" runat="server" Text="usernick" />!</h2>
    <ul class="list">
        <li>
            UserRole - <asp:Literal ID="LabelUserRole" runat="server" Text="userrole" />
        </li>        
        <li>            
            <a href="notepad.aspx" title="Посмотреть записи">Блокнот</a>
        </li>
        <li>
            <asp:Literal ID="MessagesLink" runat="server" />
        </li>
        <li>
            <a href="favorites.aspx" title="Статьи которые я отметил">Избранное</a>
        </li>       
        <li>
            <asp:HyperLink ID="AddPostLink" runat="server" NavigateUrl="~/editpost.aspx" Visible="false">Добавить пост</asp:HyperLink>
        </li>  
        <li>
            <asp:LinkButton ID="LinkButtonExit" runat="server" OnClick="LinkButtonExit_Click">Выйти</asp:LinkButton>        
        </li>
        <li>
            <asp:HyperLink ID="ManageAccountsLink" runat="server" NavigateUrl="~/accounts.aspx" Visible="false">Пользователи</asp:HyperLink>
        </li>
    </ul>   
</div>
