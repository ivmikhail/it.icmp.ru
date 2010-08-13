<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left">
    <% if (CurrentUser.isAuth) { %>
        <li>
            <% Html.RenderPartial("Link/Post/Add"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Favorite/Posts"); %>
        </li>
    <% } else { %>
        <li>
            <% Html.RenderPartial("Link/User/Register"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/User/ForgotPassword"); %>
        </li>
    <% } %>
</ul>
