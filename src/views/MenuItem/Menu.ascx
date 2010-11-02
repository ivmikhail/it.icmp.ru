<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>

<% foreach (var item in MenuItems.GetRoot()) { %>
    <dl>
        <dt><% Html.RenderPartial("Link/MenuItem/Url", item); %></dt>
        <% foreach (var child in item.Children) { %>
            <dd>
                <% Html.RenderPartial("Link/MenuItem/Url", child); %>
            </dd>
        <% } %>
    </dl>
<% } %>