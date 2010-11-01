<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Активные постеры
    <span class="period">
        <%= Config.ActivePostersTimeText %>
    </span>
</h2>

<ul>
    <% foreach (var userCountPair in Users.GetActivePosters()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", userCountPair.Key); %>
            - <span class="info"><%= userCountPair.Value%></span>
        </li>
    <% } %>
</ul>
