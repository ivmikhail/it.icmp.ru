<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Активные комментаторы
    <span class="period">
        <%= Config.ActiveCommentatorsTimeText%>
    </span>
</h2>

<ul>
    <% foreach (var userCountPair in Users.GetActiveCommentators()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", userCountPair.Key); %>
            - <span class="info"><%= userCountPair.Value%></span>
        </li>
    <% } %>
</ul>
