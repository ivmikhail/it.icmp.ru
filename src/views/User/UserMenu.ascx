<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<ul class="left-list">
    <li>
        <% Html.RenderPartial("Link/User/Profile", Model); %>
    </li>
    <% if (CurrentUser.IsAuth) { %>
        <% if (CurrentUser.User.Id != Model.Id) { %>
            <li>
                <% Html.RenderPartial("Link/Message/Send", Model); %>
            </li>
        <% } else { %>
            <li>
                <% Html.RenderPartial("Link/User/Edit", Model); %>
            </li>
        <% } %>
    <% } %>
    <li>
        <% Html.RenderPartial("Link/User/Posts", Model); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/User/Comments", Model); %>
    </li>
</ul>