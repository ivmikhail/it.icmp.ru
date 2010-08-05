<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<FavoritePostsModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    <%= CurrentUser.User.Nick %>, ваши избранные посты
</asp:Content>


<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <%= CurrentUser.User.Nick %>, ваши избранные посты
    </h1>

    <% Html.RenderPartial("PostsList", Model); %>

</asp:Content>
