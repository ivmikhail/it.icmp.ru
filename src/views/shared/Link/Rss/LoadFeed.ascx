<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<a 
    href="<%= Url.Action("load", "rss", new {id = Model.Id}) %>" title="<%= Model.Description %>">
    <%= Model.Title %></a>