<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminControl.ascx.cs" Inherits="ITCommunity.AdminControl" %>

<h1>Админ</h1>         
<ul class="list admin-panel">
    <li>
       <asp:HyperLink ID="AddPostLink" runat="server" NavigateUrl="~/editpost.aspx">Добавить пост</asp:HyperLink>
    </li> 
    <li>
       <asp:HyperLink ID="ManageAccounts" runat="server" NavigateUrl="~/accounts.aspx">Пользователи</asp:HyperLink>
    </li>
    <li>
         <a href="#" title="Добавить голосование">Голосование</a>
    </li>     
    <li>
        <a href="#" title="Править категории">Категории</a>
    </li>       
    <li>
        <a href="#" title="Править меню">Меню</a>
    </li> 
</ul>   
