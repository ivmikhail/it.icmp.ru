<%@ Control Language="C#" Inherits="ViewUserControl<Rating>" %>


рейтинг:
<b class="rating-<%= Model.Sign %>">
    <%= Model.Value %>
</b>
