<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "отправленные",
    "sents",
    "message",
    null,
    new { title = "Отправленные сообщения" }
)%>
