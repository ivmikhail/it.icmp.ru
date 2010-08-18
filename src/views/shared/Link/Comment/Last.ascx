<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ITCommunity.Db.Comment>" %>


<a  href="/post/view/<%= Model.PostId %>#comment-<%= Model.Id %>"
    title="<%= Model.TimePassed() %>"
    class="sidebar-link"><%= Model.ShortText %></a>
