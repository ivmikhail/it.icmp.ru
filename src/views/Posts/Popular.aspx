<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PostsModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    Популярные посты
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        Популярные посты за:
        <% Html.RenderPartial("Periods"); %>
    </h1>

    <% Html.RenderPartial("PostsList", Model); %>

</asp:Content>
