<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="PostListTitle" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="PostListContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Тест вывода каких либо постов</h1>

    <ul style="list-style:square;">
<% foreach (var post in (List<ITCommunity.Models.Post>)ViewData["posts"])
   { %>
   <li>
    <b><%= post.Title %></b>
    <p>
        <%= post.DescriptionFormatted %>
    </p>
   </li>
<% }; %>
</ul>
</asp:Content>