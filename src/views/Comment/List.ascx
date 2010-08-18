<%@ Control Language="C#" Inherits="ViewUserControl<List<Comment>>" %>


<ul>
    <% foreach (var comment in  Model) { %>
        <li id="comment-<%= comment.Id %>" class="light-block">
            <ul class="left-list">
                <li>
                    <% Html.RenderPartial("Link/User/Profile", comment.Author); %>
                </li>
                <li class="info meta">
                    <%= comment.CreateDate.ToString("dd MMMM yyyy, HH:mm") %>
                </li>
            </ul>
                
            <ul class="right-list meta">
                <% if (CurrentUser.IsAdmin) { %>
                    <li>
                        <% Html.RenderPartial("Link/Comment/Delete", comment); %>
                    </li>
                <% } %>
                <% if (comment.Editable) { %>
                    <li>
                        <% Html.RenderPartial("Link/Comment/Edit", comment); %>
                    </li>
                <% } %>
                <li>
                    <% Html.RenderPartial("Link/Comment/Perma", comment); %>
                </li>
            </ul>

            <div class="clear"></div>
                
            <div class="comment-text">
                <%= comment.TextFormatted %> 
            </div>
        </li>
    <% } %>
</ul>