<%@ Control Language="C#" Inherits="ViewUserControl<Comment>" %>


<a
    href="/post/view/<%= Model.PostId %>#comment-<%= Model.Id %>"
    title="<%= Model.howMuchTimeHasPassed() %>"
    class="page-link">
    #
</a>
