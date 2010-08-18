<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<UserRegisterModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Регистрация
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Регистрация</h1>

    <% using (Html.BeginForm()) { %>

        <%= Html.LabelFor(m => m.UserNick) %>
        <%= Html.TextBoxFor(m => m.UserNick) %>
        <%= Html.ValidationMessageFor(m => m.UserNick) %>

        <%= Html.LabelFor(m => m.Email) %>
        <%= Html.TextBoxFor(m => m.Email) %>
        <%= Html.ValidationMessageFor(m => m.Email) %>
                
        <%= Html.LabelFor(m => m.Password) %>
        <%= Html.PasswordFor(m => m.Password) %>
        <%= Html.ValidationMessageFor(m => m.Password) %>
                
        <%= Html.LabelFor(m => m.ConfirmPassword) %>
        <%= Html.PasswordFor(m => m.ConfirmPassword) %>
        <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                
        <% Html.RenderPartial("ITCaptcha", Model); %>

        <input type="submit" value="зарегистрироваться" />
    <% } %>

</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
    <h2>
        Заполните все поля для регистрации, пожалуйста
    </h2>
    <h2>
        Длина пароля должен быть не меньше двух символов
    </h2>
</asp:Content>
