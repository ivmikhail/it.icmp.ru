<%@ Control Language="C#" Inherits="ViewUserControl<List<SyndicationItem>>" %>


<% if (Model.Count == 0) { %>

    <h2>Упс, ничего нет</h2>

<% } else { %>

    <ul>
        <% foreach (var item in Model) { %>
            <li class="block">
                <% Html.RenderPartial("../Rss/Description", item); %>
            </li>
        <% } %>
    </ul>
    

<% } %>
