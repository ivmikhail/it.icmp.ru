<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    Обсуждаемые
</h2>

<ul>
    <% foreach (var post in Posts.GetTopDiscussibles()) { %>        
        <li>
            <% Html.RenderPartial("Link/Post/Discussible", post); %>
        </li>
    <% } %>
</ul>
