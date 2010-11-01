<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <li>
        <% Html.RenderPartial("Link/Message/Send", User.Anonymous); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Message/ReceivedList"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Message/SentList"); %>
    </li>
</ul>