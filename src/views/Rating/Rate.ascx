<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


<ul id="<%= Model.HtmlId %>" class="right-list">
    <li class="margin-none">
        <% Html.RenderPartial("Link/Rating/Up"); %>
    </li>
    <li>
       <% Html.RenderPartial("Link/Rating/Down"); %>
    </li>
</ul>