<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<h2>
    <% Html.RenderPartial("Link/Rss/LoadFeed", Model); %>
</h2>

<% if (Model.Feed == null) {%>
    <h3>Не могу загрузить!</h3>
<% } else { %>
    <ul>
        <% foreach (var item in Model.Feed.Items.Take(Config.GetInt("SidebarRssCount"))) { %>        
            <li>
                <% foreach (var author in item.Authors) { %>
                    <% Html.RenderPartial("Link/Rss/Author", author); %>
                <% } %>
                →
                <% Html.RenderPartial("Link/Rss/Title", item); %>
            </li>
        <% } %>
    </ul>
<% } %>