<%@ Control Language="C#" Inherits="ViewUserControl<Message>" %>


<%= Html.ActionLink(
    "прочитано",
    "read",
    "message",
    new { id = Model.Id },
    new { title = "Точно прочитал?" }
)%>
