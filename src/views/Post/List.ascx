<%@ Control Language="C#" Inherits="ViewUserControl<PostListModel>" %>


<% if (Model.List.Count == 0) { %>

    <h2 class="message">Упс, ничего нет</h2>

<% } else { %>

    <ul>
        <% foreach (var post in Model.List) { %>
            <li class="block">
                <h2>
                    <% Html.RenderPartial("Link/Post/Title", post); %>
                </h2>
                <div class="text">
                    <%= post.DescriptionFormatted %>
                </div>

                <div class="meta">
                    <ul class="left-list">
                        <li class="info">
                            <%= post.CreateDate.ToString("dd MMMM yyyy, HH:mm") %>
                        </li>
                        <li>
                            <% Html.RenderPartial("Link/User/Profile", post.Author); %>
                        </li>
                    </ul>

                    <ul class="right-list">
                        <li>
                            просмотров: <b class="info">~<%= post.ViewsCount %></b>
                        </li>
                        <li>
                            рейтинг: <b class="rating-none">0</b>
                        </li>
                    </ul>

                    <ul class="left-list">
                        <% foreach (var category in post.Categories) { %>
                            <li>
                                <% Html.RenderPartial("Link/Category/Posts", category); %>
                            </li>
                        <% } %>
                    </ul>

                    <ul class="right-list">
                        <% if (CurrentUser.IsAuth) { %>
                            <% if (post.IsFavorite) { %>
                                <li>
                                    <% Html.RenderPartial("Link/Favorite/Delete", post); %>
                                </li>
                            <% } else { %>
                                <li>
                                    <% Html.RenderPartial("Link/Favorite/Add", post); %>
                                </li>
                            <% } %>
                        <% } %>
                        <li>
                            <% Html.RenderPartial("Link/Post/Comments", post); %>
                        </li>
                        <li>
                            <% Html.RenderPartial("Link/Post/More", post); %>
                        </li>
                    </ul>

                    <div class="clear"></div>
                </div>
            </li>
        <% } %>
    </ul>
    
    <% Html.RenderPartial("Pagination", Model); %>

<% } %>
