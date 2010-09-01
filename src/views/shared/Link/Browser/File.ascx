<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<a 
    href="<%= Model.Link %>"
    title="<%= Model.Description %>">
    <%= Model.Name %></a>
