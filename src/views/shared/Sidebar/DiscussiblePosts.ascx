<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    <% Html.RenderPartial("Link/Post/DiscussibleList"); %>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopDiscussible()) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Discussible", post); %>
        </li>
    <% } %>
</ul>
