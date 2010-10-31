<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<h2>
    <% Html.RenderPartial("Link/Rss/LoadFeed", Model); %>
</h2>

<ul>
    <% foreach (var item in Model.Feed.Items.Take(Config.GetInt("SidebarRssCount"))) { %>        
        <li>
            <% Html.RenderPartial("Link/Rss/Title", item); %>
        </li>
    <% } %>
</ul>
