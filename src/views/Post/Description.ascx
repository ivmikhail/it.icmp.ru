<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Post>" %>

<h2>
    <% Html.RenderPartial("Link/Post/Title", Model); %>
</h2>
<div class="text">
    <%= Model.DescriptionFormatted %>
</div>

<div class="meta">
    <ul class="left-list">
        <li class="info">
            <%= Model.CreateDate.ToString("dd MMMM yyyy, HH:mm") %>
        </li>
        <li>
            <% Html.RenderPartial("Link/User/Profile", Model.Author); %>
        </li>
    </ul>

    <ul class="right-list">
        <li>
            просмотров: <b class="info">~<%= Model.ViewsCount %></b>
        </li>
        <% if (Model.Rating.IsRated) { %>
            <li>
                <% Html.RenderPartial("../Rating/Rated", Model.Rating); %>
            </li>
        <% } %>
    </ul>

    <ul class="left-list">
        <% foreach (var category in Model.Categories) { %>
            <li>
                <% Html.RenderPartial("Link/Category/Posts", category); %>
            </li>
        <% } %>
    </ul>

    <ul class="right-list">
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
        <li>
            <% Html.RenderPartial("Link/Post/Comments", Model); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Post/More", Model); %>
        </li>
    </ul>

    <div class="clear"></div>
    
</div>
