<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<a  href="<%= Url.Action("view", "post", new { id = Model.Id }) %>"
    <% if (Model.IsAttached) { %>
        class="important-link"
    <% } %>
    >
    <%= Model.TitleFormatted %></a>
