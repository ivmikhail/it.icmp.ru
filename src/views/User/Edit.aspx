<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ViewPage<UserEditModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Редактирование данных
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("UserMenu", CurrentUser.User); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Редактирование данных</h1>

    <% using (Html.BeginForm()) { %>
        
        <%= Html.ValidationSummary(true) %>

        <%= Html.LabelFor(m => m.Email) %>
        <%= Html.TextBoxFor(m => m.Email) %>
        <%= Html.ValidationMessageFor(m => m.Email) %>
                
        <%= Html.LabelFor(m => m.Password) %>
        <div class="meta">
            необязательно, если ничего не введете, то пароль не изменится
        </div>
        <%= Html.PasswordFor(m => m.Password) %>
        <%= Html.ValidationMessageFor(m => m.Password) %>
                
        <%= Html.LabelFor(m => m.ConfirmPassword) %>
        <%= Html.PasswordFor(m => m.ConfirmPassword) %>
        <%= Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                
        <%= Html.LabelFor(m => m.OldPassword) %>
        <%= Html.PasswordFor(m => m.OldPassword)%>
        <%= Html.ValidationMessageFor(m => m.OldPassword)%>
                
        <input type="submit" value="сохранить" />
    <% } %>

</asp:Content>
