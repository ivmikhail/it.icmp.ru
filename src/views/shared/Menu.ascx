<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <% if (CurrentUser.IsAuth) { %>
        <li>
            <% Html.RenderPartial("Link/Post/Add"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Header/Add"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Favorite/Posts"); %>
        </li>
    <% } %>
    <li>
        <% Html.RenderPartial("Link/Post/DiscussibleList"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Post/PopularList"); %>
    </li>
</ul>
