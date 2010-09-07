<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "poll",
    new { id = Model.Id },
    new { title = "Удалить опрос", @class = "delete-link" }
)%>
