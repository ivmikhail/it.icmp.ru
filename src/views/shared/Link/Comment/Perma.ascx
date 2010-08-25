<%@ Control Language="C#" Inherits="ViewUserControl<Comment>" %>


<a  href="<%= Url.Action("view", "post", new { id = Model.PostId })%>#comment-<%= Model.Id %>"
    title="<%= Model.TimePassed() %>"
    class="page-link">#</a>
