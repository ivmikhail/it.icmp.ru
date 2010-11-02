<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


<% if (Model.IsRated) { %>
    <% Html.RenderPartial("../Rating/Rating", Model); %>
<% } else { %>
    <ul id="<%= Model.HtmlId %>" class="right-list">
        <li class="margin-none">
            <% Html.RenderPartial("Link/Rating/Up"); %>
        </li>
        <li>
            <% Html.RenderPartial("Link/Rating/Down"); %>
        </li>
    </ul>
<% } %>
