<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Топ постеров</h2>

<ul>
    <% foreach (var userCountPair in Users.GetTopPosters()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", userCountPair.Key); %>
            за все время написал постов:
            <span class="info"><%= userCountPair.Value%></span>
        </li>
    <% } %>
</ul>
