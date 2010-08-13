<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Популярные посты 
    <% Html.RenderPartial("Link/Post/PopularMore"); %>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopPopular() ) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Popular", post); %>
        </li>
    <% } %>
</ul>   
