<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Последние комментарии</h2>

<ul>
    <% foreach (var comment in Comments.GetLast()) { %>        
        <li>

            <% Html.RenderPartial("Link/User/Profile", comment.Author); %>

            &rarr;

            <% Html.RenderPartial("Link/Comment/Last", comment); %>

        </li>
    <% } %>
</ul>
