<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <li>
        <% Html.RenderPartial("Link/Header/List"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Captcha/List"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/MenuItem/List"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Category/List"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/User/List"); %>
    </li>
</ul>