<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% foreach (var rss in Rsses.All) { %>
    <% Html.RenderPartial("Sidebar/Rss", rss); %>
<% } %>
