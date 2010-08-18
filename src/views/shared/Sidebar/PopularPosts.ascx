<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Популярные посты
</h2>

<ul>
    <% foreach (var post in Posts.GetTopPopulars() ) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Popular", post); %>
        </li>
    <% } %>
</ul>   
