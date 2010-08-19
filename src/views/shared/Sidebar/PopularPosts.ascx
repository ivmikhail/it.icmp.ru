<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Популярные посты
    <span class="period">
        <%= Config.Get("PopularPostsTimeText")%>
    </span>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopPopular() ) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Popular", post); %>
        </li>
    <% } %>
</ul>   
