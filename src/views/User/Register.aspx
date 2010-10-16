<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<UserRegisterModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Регистрация
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Регистрация</h1>

    <% using (Html.BeginForm()) { %>

        <%= Html.ValidationSummary(true) %>

        <%= Html.LabelFor(m => m.UserNick) %>
        <%= Html.TextBoxFor(m => m.UserNick) %>
        <%= Html.ValidationMessageFor(m => m.UserNick) %>

        <%= Html.LabelFor(m => m.Email) %>        
        <div class="meta">Нигде не публикуется. Нужна на случай, если вы забудете пароль</div>
        <%= Html.TextBoxFor(m => m.Email) %>        
        <%= Html.ValidationMessageFor(m => m.Email) %>
                
        <%= Html.LabelFor(m => m.Password) %>
        <%= Html.PasswordFor(m => m.Password) %>
        <%= Html.ValidationMessageFor(m => m.Password) %>
                
        <%= Html.LabelFor(m => m.ConfirmPassword) %>
        <%= Html.PasswordFor(m => m.ConfirmPassword) %>
        <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                
        <% Html.RenderPartial("../Captcha/Captcha", Model); %>

        <input type="submit" value="зарегистрироваться" />
    <% } %>

</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">
<div class="text">
    <h3>
        Зарегистрировашись, вы получаете
    </h3>
    <ul class="list">
        <li>Доступ к различным файлам</li>
        <li>Доступ к видео/аудио материалам</li>
        <li>Вы можете опубликовать свою новость или опрос</li>
        <li>Доступ к различного рода сервисам</li>
    </ul>

    <h3>
        Для регистрации нужно
    </h3>
    <ul class="list">
        <li>Заполнить все поля слева</li>
        <li>Длина пароля должна состоять минимум из 2-х символов</li>
    </ul>
</div>
</asp:Content>
