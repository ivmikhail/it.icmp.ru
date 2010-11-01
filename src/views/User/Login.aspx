<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<UserLoginModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Вход
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Вход</h1>

    <% using (Html.BeginForm()) { %>

        <%= Html.ValidationSummary(true)%>

        <%= Html.LabelFor(m => m.UserNick) %>
        <%= Html.TextBoxFor(m => m.UserNick) %>
        <%= Html.ValidationMessageFor(m => m.UserNick) %>
                
        <%= Html.LabelFor(m => m.Password) %>
        <%= Html.PasswordFor(m => m.Password) %>
        <%= Html.ValidationMessageFor(m => m.Password) %>

        <label>
            <%= Html.CheckBoxFor(m => m.RememberMe) %> запомнить?
        </label>
                
        <input type="submit" value="войти" />

    <% } %>
</asp:Content>

<asp:Content ID="Sidebar" ContentPlaceHolderID="SidebarContent" runat="server">

    <h2>
        Если у вас нет аккаунта, вы можете <% Html.RenderPartial("Link/User/Register"); %>
    </h2>

</asp:Content>
