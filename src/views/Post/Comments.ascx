<%@ Control Language="C#" Inherits="ViewUserControl<List<Comment>>" %>


<h2 id="comments">Комментарии (<%= Model.Count %>)</h2>

    <ul>
        <% foreach (var comment in  Model) { %>
            <li id="comment-<%= comment.Id %>" class="menu comment ">
                <ul class="left">
                    <li>
                        <% Html.RenderPartial("Link/User/Profile", comment.getAuthor()); %>
                    </li>
                    <li class="date meta">
                        <%= comment.CreateDate.ToString("dd MMMM yyyy, HH:mm") %>
                    </li>
                </ul>
                
                <ul class="right meta">
                    <li>
                        <% if (comment.Editable) { %>
                             <% Html.RenderPartial("Link/Comment/Edit", comment); %>
                        <% } %>
                    </li>
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