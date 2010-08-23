<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>
    <% Html.RenderPartial("Link/Post/DiscussibleList"); %>
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
