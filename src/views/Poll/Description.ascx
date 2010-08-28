<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>

<h2>
    <% Html.RenderPartial("Link/Post/Title", Model); %>
</h2>
<div class="text">
    <% Html.RenderPartial("../Poll/Answers", Model.Entity); %>
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
        <li>
            <% Html.RenderPartial("../Poll/IsOpen", Model.Entity); %>
        </li>
        <li>
            <% Html.RenderPartial("../Poll/EndDate", Model.Entity); %>
        </li>
        <li>
            <% Html.RenderPartial("../Poll/VotesCount", Model.Entity); %>
        </li>
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
