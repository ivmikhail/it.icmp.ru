<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<h2>
    <% Html.RenderPartial("Link/Post/Title", Model); %>
</h2>

<div class="text">
    <%= Model.DescriptionFormatted %>
</div>

<div class="meta">
    <% Html.RenderPartial("../Post/Meta", Model); %>
</div>
