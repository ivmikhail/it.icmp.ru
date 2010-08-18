<%@ Page Language="C#" MasterPageFile="~/views/shared/Site.master" Inherits="ViewPage<UserNickModel>" %>


<asp:Content ID="SendTitle" ContentPlaceHolderID="TitleContent" runat="server">
     Забыли пароль?
</asp:Content>

<asp:Content ID="SendContent" ContentPlaceHolderID="MainContent" runat="server">    
    <h1>Забыли пароль?</h1>

    <% using (Html.BeginForm()) { %>

        <%= Html.LabelFor(m => m.UserNick) %>
        <%= Html.TextBoxFor(m => m.UserNick) %>
        <%= Html.ValidationMessageFor(m => m.UserNick) %>

        <input type="submit" value="отправить"/>
    <% } %>

</asp:Content>

<asp:Content ID="SendSidebar" ContentPlaceHolderID="SidebarContent" runat="server">    
    
    <%= Html.ValidationSummary(true, "Письмо не отправлено. Попробуйте еще раз. Если все равно не работает, обратитесь к администрации") %>

    <h2>
        Введите Ваш ник, ссылка для сброса пароля будет отправлена на ваш e-mail указанный при регистрации
    </h2>

</asp:Content>

