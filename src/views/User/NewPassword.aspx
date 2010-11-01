<%@ Page Language="C#" MasterPageFile="~/views/shared/Site.master" Inherits="ViewPage<UserNewPasswordModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Задание нового пароля
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Ваш логин : <%= Model.UserNick %></h1>

    <% using (Html.BeginForm()) { %>
         
        <%= Html.HiddenFor(m => m.UserNick) %>

        <%= Html.LabelFor(m => m.Password ) %>
        <%= Html.PasswordFor(m => m.Password) %>
        <%= Html.ValidationMessageFor(m => m.Password) %>

        <%= Html.LabelFor(m => m.ConfirmPassword) %>
        <%= Html.PasswordFor(m => m.ConfirmPassword) %>
        <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>

        <input type="submit" value="Изменить"/>

    <% } %>

</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
    <h2>
        Длина пароля должен быть не меньше двух символов
    </h2>
</asp:Content>
