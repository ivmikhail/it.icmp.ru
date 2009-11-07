<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="ITCommunity.UserControl" %>
<div id="user_profile">
    <h1><asp:Literal ID="LabelUserLogin" runat="server" Text="usernick" />!</h1>
    <ul class="list user-profile">
        <li>
            UserRole - <asp:Literal ID="LabelUserRole" runat="server" Text="userrole" />
        </li>   
        <li>        
            <a href="editpost.aspx"" title="Добавить свою новость">Добавить пост</a>
       </li> 
        <li>
            <asp:Literal ID="MessagesLink" runat="server" />
        </li>    
        <li>            
            <a href="notepad.aspx" title="Посмотреть записи">Блокнот</a>
        </li>
        <li>
            <a href="favorites.aspx" title="Статьи которые я отметил">Избранное</a>
        </li>    
        <li>
            <a href="profile.aspx" title="Изменить email или пароль">Профиль</a>
        </li>    
        <li>
            <asp:LinkButton ID="LinkButtonExit" runat="server" OnClick="LinkButtonExit_Click">Выйти</asp:LinkButton>        
        </li>
    </ul>   
</div>
