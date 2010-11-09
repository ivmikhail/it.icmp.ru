<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<% if (Model.PostsLike.Count > 0 ) {%>
    <h2>Похожие новости:</h2>
    <ul>
        <% foreach(var post in Model.PostsLike) { %>
            <li class="light-block">
                <%= Html.ActionLink(string.IsNullOrEmpty(post.Title) ? "Без названия" : post.Title, "View", new { id = post.Id })%>
            </li>
        <% } %>
    </ul>
<% } %>