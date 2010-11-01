<%@ Control Language="C#" Inherits="ViewUserControl<string>" %>


<%= Html.ActionLink(
    "удалить все",
    "deleteall",
    "message",
    new { messages = Model },
    new { title = "Удалить все эти сообщения", @class = "delete-link" }
)%>
