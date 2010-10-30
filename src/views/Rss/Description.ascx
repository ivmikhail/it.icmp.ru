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
            <%= Html.Date(Model.LastUpdatedTime.DateTime) %>
        </li>
        <li>
            <% foreach (var author in Model.Authors) { %>
                <%= author.Name %>
            <% } %>
        </li>
    </ul>

    <ul class="right-list">
        <li>
            
        </li>
    </ul>

    <ul class="left-list">
        <li>
        </li>
    </ul>

    <ul class="right-list">
        <li></li>
    </ul>

    <div class="clear"></div>
    
</div>
