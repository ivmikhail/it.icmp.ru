<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<h2>
    <% Html.RenderPartial("Link/Post/Title", Model); %>
</h2>
<div class="text">
    <% Html.RenderPartial("../Poll/Answers", Model.Entity); %>
</div>

<div class="meta">
    <ul class="left-list">
        <li>
            <% Html.RenderPartial("../Poll/EndDate", Model.Entity); %>
        </li>
    </ul>
    <ul class="right-list">
        <li>
            <% Html.RenderPartial("../Poll/IsOpen", Model.Entity); %>
        </li>
        <li>
            <% Html.RenderPartial("../Poll/VotedUsersCount", Model.Entity); %>
        </li>
    </ul>
    <ul class="left-list">
        <li class="info">
            <%= Html.Date(Model.CreateDate) %>
        </li>
        <li>
            <% Html.RenderPartial("Link/User/Profile", Model.Author); %>
        </li>
    </ul>

    <ul class="right-list">
        <li>просмотров: <b class="info">~<%= Model.ViewsCount %></b> </li>
        <li>
            <% if (Model.AuthorId == CurrentUser.User.Id) { %>
                <% Html.RenderPartial("../Rating/AuthorRating", Model.Rating); %>
            <% } else { %>
                <% Html.RenderPartial("../Rating/Rating", Model.Rating); %>
            <% } %>
        </li>
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
        <% if (Model.IsCommentable) { %>
            <li>
                <% Html.RenderPartial("Link/Post/Comments", Model); %>
            </li>
        <% } %>
        <li>
            <% Html.RenderPartial("Link/Post/More", Model); %>
        </li>
    </ul>

    <div class="clear"></div>
    
</div>
