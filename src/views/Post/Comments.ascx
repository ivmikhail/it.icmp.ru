<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<% if (Model.IsCommentable) { %>
    <h2 id="comments">Комментарии (<%= Model.CommentsCount%>)</h2>

    <% Html.RenderPartial("../Comment/List", Model.Comments.ToList()); %>

    <% if (CurrentUser.IsAuth) { %>
        <% Html.RenderPartial("../Comment/Add", new CommentEditModel { PostId = Model.Id }); %>
    <% } else { %>
        <% Html.RenderPartial("../Comment/AnonymousAdd", new AnonymousCommentAddModel { PostId = Model.Id }); %>
    <% } %>
<% } else { %>
    <h2 class="none-active">Комментарии отключены</h2>
<% } %>
