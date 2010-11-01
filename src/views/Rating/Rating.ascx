<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


рейтинг:
<% if (Model.IsRated) { %>
    <b class="rating-<%= Model.Sign %>"><%= Model.Value %></b>
<% } else { %>
    <b class="info">?</b>
<% } %>
