<%@ Control Language="C#" Inherits="ViewUserControl<Comment>" %>


<a  href="<%= Url.Action("view", "post", new { id = Model.PostId })%>#comment-<%= Model.Id %>"
    title="<%= Model.Post.TitleFormatted %>"
    class="sidebar-link"><%= Model.ShortText %></a>
