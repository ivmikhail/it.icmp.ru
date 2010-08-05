<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


 <% if (CurrentUser.isAuth) { %>
    <ul class="left">
        <li>
            <%= Html.ActionLink("написать пост", "add", "posts") %>
        </li>
        <li>
            <%= Html.ActionLink("избранные", "favorite", "posts")%>
        </li>
    </ul>
<% } %>
