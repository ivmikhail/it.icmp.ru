<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    <% Html.RenderPartial("Link/Post/PopularList"); %>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopPopular() ) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Popular", post); %>
        </li>
    <% } %>
</ul>   
