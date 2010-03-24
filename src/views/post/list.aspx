<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="PostListTitle" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="PostListContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>test</h1>

<% foreach (var post in (List<ITCommunity.Models.Post>)ViewData["posts"])
   { %>
   <%= post.Title %>
<% }; %>

</asp:Content>