<%@ Control Language="C#" Inherits="ViewUserControl<PaginatedModel>" %>


<ul class="pagination">
    <li>
        <% if (Model.Page == 1) { %>
            <span>назад</span>
        <% } else { %>
            <a href="?page=<%= Model.Page - 1 %><%= Html.Encode(Model.Query) %>">назад</a>
        <% } %>
    </li>
    <% for (int i = Model.StartPage; i <= Model.EndPage; i++) { %>
        <li>
            <% if (i == Model.Page) { %>
                <span class="current-page"><%= i%></span>
            <% } else { %>
                <a href="?page=<%= i %><%= Html.Encode(Model.Query) %>"><%= i%></a>
            <% } %>
        </li>
    <% } %>
    <li>
        <% if (Model.Page >= Model.PagesCount) { %>
            <span>вперед</span>
        <% } else { %>
            <a href="?page=<%= Model.Page + 1 %><%= Html.Encode(Model.Query) %>">вперед</a>
        <% } %>
    </li>
</ul>
