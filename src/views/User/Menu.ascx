<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <li>
        <% Html.RenderPartial("Link/User/Login"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/User/Register"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/User/ForgotPassword"); %>
    </li>
</ul>