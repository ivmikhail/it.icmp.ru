<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<UserCommentsModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Комментарии (<%= Model.TotalCount %>) - <%= Model.User.Nick %>
</asp:Content>

<asp:Content ID="Menu" ContentPlaceHolderID="MenuContent" runat="server">
    <% Html.RenderPartial("UserMenu", Model.User); %>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Комментарии (<%= Model.TotalCount %>) - <%= Model.User.Nick %></h1>

    <% Html.RenderPartial("../Comment/List", Model.List); %>

    <% Html.RenderPartial("Pagination", Model); %>

</asp:Content>
