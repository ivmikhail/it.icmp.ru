<%@ Control Language="C#" Inherits="ViewUserControl<SyndicationItem>" %>


<h2>
    <% Html.RenderPartial("Link/Rss/Title", Model); %>
</h2>
<div class="text">
    <% if (Model.Summary != null) { %>
        <%= Model.Summary.Text%>
    <% } else { %>
        <%= ((TextSyndicationContent)Model.Content).Text%>
    <% } %>
</div>

<div class="meta">
    <ul class="left-list">
        <li class="info">
            <% if (Model.PublishDate != DateTimeOffset.MinValue) { %>
                <%= Html.Date(Model.PublishDate.DateTime)%>
            <% } else if (Model.LastUpdatedTime != DateTimeOffset.MinValue) { %>
                <%= Html.Date(Model.LastUpdatedTime.DateTime)%>
            <% } else { %>
                дата публикации неизвестна
            <% } %>
        </li>
        <li>
            <% foreach (var author in Model.Authors) { %>
                <% Html.RenderPartial("Link/Rss/Author", author); %>
            <% } %>
        </li>
    </ul>

    <% if (Model.Categories.Count != 0) { %>
        <ul class="right-list">
            <% foreach (var category in Model.Categories) { %>
                <li>
                    <%= category.Name%>
                </li>
            <% } %>
        </ul>
    <% } %>

    <div class="clear"></div>
    
</div>
