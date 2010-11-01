<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "rss",
    new { id = Model.Id },
    new { title = "Удалить загружаемую rss-ку", @class = "delete-link" }
)%>
