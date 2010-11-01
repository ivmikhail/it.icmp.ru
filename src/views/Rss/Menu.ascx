<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <% foreach (var rss in Rsses.All) { %>
        <li>
            <% Html.RenderPartial("Link/Rss/LoadFeed", rss); %>
        </li>
    <% } %>
</ul>