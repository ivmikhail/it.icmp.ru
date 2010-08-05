<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<CategoryPostsModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Посты категории: <%= Model.Category.Name %>
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Посты категории: <%= Model.Category.Name %></h1>

    <% Html.RenderPartial("PostsList", Model); %>

</asp:Content>
