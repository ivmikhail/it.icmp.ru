<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Новые пользователи</h2>

<ul>
    <% foreach (var user in Users.GetLastRegistered()) { %>        
        <li>
            <% Html.RenderPartial("Link/User/Profile", user); %>
            - <span class="info"><%= user.CreateDate.ToString("dd MMMM")%></span>
        </li>
    <% } %>
</ul>
