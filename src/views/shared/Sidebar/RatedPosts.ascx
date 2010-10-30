<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Рейтинговые
    <span class="period">
        <%= Config.RatedPostsTimeText%>
    </span>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopRated() ) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Popular", post); %>
        </li>
    <% } %>
</ul>   
