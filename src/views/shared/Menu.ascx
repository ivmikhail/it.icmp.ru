<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <% if (CurrentUser.IsAuth) { %>
        <li>
            <% Html.RenderPartial("Link/Post/Add"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Post/AddPoll"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Header/Add"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Favorite/Posts"); %>
        </li>
    <% } else { %>
        <li>
            <% Html.RenderPartial("Link/User/Login"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/User/Register"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/User/ForgotPassword"); %>
        </li>
    <% } %>
</ul>
