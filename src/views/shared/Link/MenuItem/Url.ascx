<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.DB.MenuItem>" %>


<a  href="<%= Model.Url %>" 
    title="<%= Model.Url %>" <% if(Model.IsTargetBlank) { %> target="_blank" <% } %>><%= Model.Name %></a>
