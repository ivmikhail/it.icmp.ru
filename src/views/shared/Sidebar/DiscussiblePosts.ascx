<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Обсуждаемые
    <span class="period">
        <%= Config.Get("DiscussiblePostsTimeText")%>
    </span>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopDiscussible()) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Discussible", post); %>
        </li>
    <% } %>
</ul>
