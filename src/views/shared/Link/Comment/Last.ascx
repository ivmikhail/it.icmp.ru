<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ITCommunity.Db.Comment>" %>


<a
    href="/post/view/<%= Model.PostId %>#comment-<%= Model.Id %>"
    title="<%= Model.howMuchTimeHasPassed() %>"
    class="sidebar-link">
    <%= Model.ShortText %>
</a>
