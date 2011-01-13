<%@ Control Language="C#" Inherits="ViewUserControl<BrowseItem>" %>


<a 
    href="<%= Model.Link %>"
    title="<%= Model.Name %>">
    <%= Model.ShortName%></a>
