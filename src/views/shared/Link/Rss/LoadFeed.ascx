<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<a 
    href="<%= Url.Action("load", "rss", new {id = Model.Id}) %>" title="<%= Model.Feed.Description.Text %>">
    <%= Model.Title %></a>