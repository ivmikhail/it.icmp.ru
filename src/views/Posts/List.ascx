<%@ Control Language="C#" Inherits="ViewUserControl<PostsModel>" %>


<% if (Model.List.Count == 0) { %>

    <h2 class="message">Упс, ничего нет</h2>

<% } else { %>

    <ul>
        <% foreach (var post in Model.List) { %>
            <li class="post">
                <h2>
                    <% Html.RenderPartial("Link/Post/Title", post); %>
                </h2>
                <div class="post-description">
                    <%= post.DescriptionFormatted %>
                </div>

                <div class="post-meta">
                    <div class="menu">
                        <ul class="left">
                            <li><span class="date"><%= post.CreateDate.ToString("dd MMMM yyyy, HH:mm") %></span></li>
                            <li><% Html.RenderPartial("Link/User/Profile", post.Author); %></li>
                        </ul>

                        <ul class="right">
                            <li>просмотров: <b class="views-count">~<%= post.ViewsCount %></b></li>
                            <li>рейтинг: <b class="rating-none">0</b></li>
                        </ul>
                    </div>

                    <div class="menu">
                        <ul class="left">
                            <% foreach (var category in post.Categories) { %>
                                <li><% Html.RenderPartial("Link/Category/Posts", category); %></li>
                            <% } %>
                        </ul>

                        <ul class="right">
                            <% if (CurrentUser.isAuth) {
                                if (post.IsFavorite) { %>
                                    <li>
                                        <% Html.RenderPartial("Link/Favorite/Delete", post); %>
                                    </li>
                                <% } else { %>
                                    <li>
                                        <% Html.RenderPartial("Link/Favorite/Add", post); %>
                                    </li>
                                <%} %>
                            <% } %>
                            <li><% Html.RenderPartial("Link/Post/Comments", post); %></li>
                            <li><% Html.RenderPartial("Link/Post/More", post); %></li>
                        </ul>
                    </div>
                </div>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>
    
    <% Html.RenderPartial("Pagination", Model); %>

<% } %>