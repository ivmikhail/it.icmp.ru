<%@ Control Language="C#" Inherits="ViewUserControl<PostsModel>" %>


<% if (Model.List.Count == 0) { %>

    <h2 class="message">Упс, ничего нет</h2>

<% } else { %>

    <ul>
        <% foreach (var post in Model.List) { %>
            <li class="post">
                <h2>
                    <%= Html.ActionLink(
                        post.TitleFormatted,
                        "view",
                        "posts",
                        new { id = post.Id },
                        null
                    )%>
                </h2>
                <div class="post-description">
                    <%= post.DescriptionFormatted %>
                </div>

                <div class="post-meta">
                    <div class="menu">
                        <ul class="left">
                            <li><span class="date"><%= post.CreateDate.ToString("dd MMMM yyyy, HH:mm") %></span></li>
                            <li><% Html.RenderPartial("UserLink", post.GetAuthor()); %></li>
                        </ul>

                        <ul class="right">
                            <li>просмотров: <b class="views-count">~<%= post.ViewsCount %></b></li>
                            <li>рейтинг: <b class="rating-none">0</b></li>
                        </ul>
                    </div>

                    <div class="menu">
                        <ul class="left">
                            <% foreach (var category in post.Categories) { %>
                                <li><% Html.RenderPartial("CategoryLink", category); %></li>
                            <% } %>
                        </ul>

                        <ul class="right">
                            <% if (CurrentUser.isAuth) {
                                if (post.IsFavorite) { %>
                                    <li>
                                        <%= Html.ActionLink(
                                            "убрать",
                                            "deletefavorite",
                                            "posts",
                                            new { id = post.Id },
                                            new { title = "Убрать из избранных" }
                                        )%>
                                    </li>
                                <% } else { %>
                                    <li>
                                        <%= Html.ActionLink(
                                            "добавить",
                                            "addfavorite",
                                            "posts",
                                            new { id = post.Id },
                                            new { title = "Добавить в избранные" }
                                        )%>
                                    </li>
                                <%} %>
                            <% } %>
                            <li><a href="/posts/view/<%= post.Id %>#comments" title="Посмотреть комментарии">комментарии (<%= post.CommentsCount %>)</a></li>
                            <li><a href="/posts/view/<%= post.Id %>#cut" title="Читать далее" class="main-link">дальше</a></li>
                        </ul>
                    </div>
                </div>
                <div class="clear"></div>
            </li>
        <% } %>
    </ul>

    <% Html.RenderPartial("Pagination", Model); %>

<% } %>