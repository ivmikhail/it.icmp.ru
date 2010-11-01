<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<dynamic>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Ваши данные сохранены
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("UserMenu", CurrentUser.User); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Ваши данные сохранены</h1>

</asp:Content>
