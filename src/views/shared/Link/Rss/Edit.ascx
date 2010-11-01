<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<%= Html.ActionLink(
    "редактировать",
    "list",
    "rss",
    new { id = Model.Id },
    new { title = "Редактировать загружаемую rss-ку" }
)%>
