<%@ Control Language="C#" Inherits="ViewUserControl<PostListModel>" %>


<% if (Model.List.Count == 0) { %>

    <h2>Упс, ничего нет</h2>

<% } else { %>

    <ul>
        <% foreach (var post in Model.List) { %>
            <li class="block">
                <% Html.RenderPartial("Description", post); %>
            </li>
        <% } %>
    </ul>
    
    <% Html.RenderPartial("Pagination", Model); %>

<% } %>
