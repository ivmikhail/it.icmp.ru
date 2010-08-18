<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Активные комментаторы</h2>

<ul>
    <% foreach (var userCountPair in Users.GetActiveCommentators()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", userCountPair.Key); %>
            за <%= Config.Get("ActiveCommentatorsTimeText")%> написал комментариев:
            <span class="info"><%= userCountPair.Value%></span>
        </li>
    <% } %>
</ul>
