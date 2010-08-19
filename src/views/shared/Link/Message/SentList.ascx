<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "отправленные",
    "sentlist",
    "message",
    null,
    new { title = "Отправленные сообщения" }
)%>
