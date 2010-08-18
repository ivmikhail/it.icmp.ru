<%@ Control Language="C#" Inherits="ViewUserControl<ITCommunity.Db.MenuItem>" %>


<a  href="<%= Model.Url %>"
    title="<%= Model.Url %>"><%= Model.Name %></a>
