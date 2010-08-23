<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


<ul id="<%= Model.HtmlId %>" class="right-list">
    <li>
        <% Html.RenderPartial("Link/Rating/Up"); %>
    </li>
    <li>
       <% Html.RenderPartial("Link/Rating/Down"); %>
    </li>
</ul>