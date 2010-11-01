<%@ Page Language="C#" MasterPageFile="~/views/shared/Site.master" Inherits="ViewPage<UserNickModel>" %>


<asp:Content ID="SendTitle" ContentPlaceHolderID="TitleContent" runat="server">
     Письмо отправлено
</asp:Content>

<asp:Content ID="SendContent" ContentPlaceHolderID="MainContent" runat="server">    

    <h1>Письмо отправлено, <%= Model.UserNick %>!</h1>

</asp:Content>

<asp:Content ID="SendSidebar" ContentPlaceHolderID="SidebarContent" runat="server">    
    <h2>
        Ссылка для задания нового пароля отправлена на ваш e-mail указанный при регистрации
    </h2>
</asp:Content>

