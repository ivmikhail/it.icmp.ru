<%@ Control Language="C#" Inherits="ViewUserControl<Message>" %>


<%= Html.ActionLink(
    "ответить",
    "reply",
    "message",
    new { id = Model.Id },
    new { title = "Написать ответное сообщение", @class = "main-link" }
)%>
