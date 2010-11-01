<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>

голосование:
<b class="info">
    <% if (Model.IsOpen) { %>
        открытое
    <% } else { %>
        закрытое
    <% } %>
</b>