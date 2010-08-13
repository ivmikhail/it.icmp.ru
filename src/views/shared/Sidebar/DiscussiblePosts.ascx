<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Обсуждаемое
    <% Html.RenderPartial("Link/Post/DiscussibleMore"); %>
</h2>

<ul>
    <% foreach (var post in Posts.GetTopDiscussible()) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Discussible", post); %>
        </li>
    <% } %>
</ul>
