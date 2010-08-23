<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.DB.MenuItem>" %>


<a  href="<%= Model.Url %>"
    title="<%= Model.Url %>"><%= Model.Name %></a>
