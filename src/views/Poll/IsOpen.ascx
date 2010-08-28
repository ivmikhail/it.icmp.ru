<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>

голосование:
<span class="info">
    <% if (Model.IsOpen) { %>
        открытое
    <% } else { %>
        закрытое
    <% } %>
</span>