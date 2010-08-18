<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<ul class="left-list">
    <li>
        <% Html.RenderPartial("Link/Message/Send", User.Anonymous); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Message/Unreads"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Message/Reads"); %>
    </li>
    <li>
        <% Html.RenderPartial("Link/Message/Sents"); %>
    </li>
</ul>