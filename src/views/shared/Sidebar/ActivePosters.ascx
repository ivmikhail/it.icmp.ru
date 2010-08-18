<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Активные постеры</h2>

<ul>
    <% foreach (var userCountPair in Users.GetActivePosters()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", userCountPair.Key); %>
            за <%= Config.Get("ActivePostersTimeText") %> написал постов:
            <span class="info"><%= userCountPair.Value%></span>
        </li>
    <% } %>
</ul>
