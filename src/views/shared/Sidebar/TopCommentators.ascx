<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Топ комментаторов</h2>

<ul>
    <% foreach (var userCountPair in Users.GetTopPosters()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", userCountPair.Key); %>
            - <span class="info"><%= userCountPair.Value %></span>
        </li>
    <% } %>
</ul>
