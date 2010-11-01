<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.master" Inherits="ViewPage<PostListModel>" %>


<asp:Content ID="Title" ContentPlaceHolderID="TitleContent" runat="server">
    IT Community - Главная
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <% Html.RenderPartial("List", Model); %>

</asp:Content>
