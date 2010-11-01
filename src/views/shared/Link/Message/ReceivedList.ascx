<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "полученные",
    "receivedlist",
    "message",
    null,
    new { title = "Полученные сообщения" }
)%>
