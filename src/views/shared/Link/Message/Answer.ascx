<%@ Control Language="C#" Inherits="ViewUserControl<Message>" %>


<%= Html.ActionLink(
    "ответить",
    "send",
    "message",
    new { receiver = Model.Sender.Nick },
    new { title = "Написать ответное сообщение", @class = "main-link" }
)%>
