<%@ Control Language="C#" Inherits="ViewUserControl<Poll>" %>


<%= Html.ActionLink(
    "редактировать",
    "edit",
    "poll",
    new { id = Model.Id },
    new { title = "Редактировать опрос" }
)%>
