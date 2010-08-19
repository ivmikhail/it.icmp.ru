<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<h2>Категории</h2>

<ul>
    <% foreach (var category in Categories.GetAll()) { %>        
        <li>
            <% Html.RenderPartial("Link/Category/Posts", category); %>
        </li>
    <% } %>
</ul>
