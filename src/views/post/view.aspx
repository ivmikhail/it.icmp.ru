<%@ Page Title="" Language="C#" MasterPageFile="~/views/shared/site.master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Просмотр сообщения
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h1><%= ((ITCommunity.Models.Post)ViewData["post"]).TitleFormatted %></h1>

<div>
    <%= ((ITCommunity.Models.Post)ViewData["post"]).TextFormatted %>
</div>
<ul style="list-style:square;">
<% foreach (var comment in (List<ITCommunity.Models.Comment>)ViewData["comments"])
   { %>
   <li>
    <b><%= comment.Author.Login %></b>
    <p>
       <%= comment.Text %> 
    </p>
   </li>
<% }; %>
</ul>
</asp:Content>
