<%@ Control Language="C#" Inherits="ViewUserControl<Message>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "message",
    new { id = Model.Id },
    new { title = "Удалить сообщение", @class = "delete-link" }
)%>
