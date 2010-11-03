<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<ul class="left-list">
    <li class="info">
        <%= Html.Date(Model.CreateDate) %>
    </li>
    <li>
        <% Html.RenderPartial("Link/User/Profile", Model.Author); %>
    </li>
</ul>
<ul class="right-list">
    <li>
        просмотров: <b class="info">~<%= Model.ViewsCount%></b>
    </li>
    <li>
        <% if (CurrentUser.IsAuth) { %>
            <% if (Model.AuthorId == CurrentUser.User.Id) { %>
                <% Html.RenderPartial("../Rating/AuthorRating", Model.Rating); %>
            <% } else { %>
                <% Html.RenderPartial("../Rating/Rate", Model.Rating); %>
            <% } %>
        <% } else { %>
            <% Html.RenderPartial("../Rating/Rating", Model.Rating); %>
        <% } %>
    </li>
</ul>

<div class="clear"></div>

<ul class="left-list">
    <% if (Model.Categories.Count == 0) { %>
        <li>
            категории не заданы
        </li>
    <% } else { %>
        <% foreach (var category in Model.Categories) { %>
        <li>
            <% Html.RenderPartial("Link/Category/Posts", category); %>
        </li>
        <% } %>
    <% } %>
</ul>
<ul class="right-list">
    <% if (Model.Editable) { %>
        <li>
            <% Html.RenderPartial("Link/Post/Delete", Model); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Post/Edit", Model); %>
        </li>
    <% } %>
    <% if (CurrentUser.IsAuth) { %>
        <% if (Model.IsFavorite) { %>
            <li>
                <% Html.RenderPartial("Link/Favorite/Delete", Model); %>
            </li>
        <% } else { %>
            <li>
                <% Html.RenderPartial("Link/Favorite/Add", Model); %>
            </li>
        <% } %>
    <% } %>
    <% if (!string.IsNullOrEmpty(Model.Source)) { %>
        <li>
            <% Html.RenderPartial("Link/Post/Source", Model); %>
        </li>
    <% } %>
</ul>

<div class="clear"></div>
