<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminControl.ascx.cs" Inherits="ITCommunity.AdminControl" %>

<h1>Что-нибудь сделать</h1>         
<ul class="list admin-panel">
    <li>
       <asp:HyperLink ID="AddPostLink" runat="server" NavigateUrl="~/editpost.aspx">Добавить пост</asp:HyperLink>
    </li> 
    <li>
       <asp:HyperLink ID="ManageAccounts" runat="server" NavigateUrl="~/accounts.aspx">Пользователи</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="AddPollLink" runat="server" NavigateUrl="~/addpoll.aspx">Голосование</asp:HyperLink>
    </li>     
    <li>
        <asp:HyperLink ID="SiteStructureLink" runat="server" NavigateUrl="~/structure.aspx">Меню/Категории</asp:HyperLink>
    </li>       
    <li>
        <asp:HyperLink ID="ItCaptchaEditor" runat="server" NavigateUrl="~/itcaptchalist.aspx">Каптчи</asp:HyperLink>
    </li> 
</ul>   
