<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="PostListTitle" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="PostListContent" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.RenderPartial("posts-list"); %>
</asp:Content>
