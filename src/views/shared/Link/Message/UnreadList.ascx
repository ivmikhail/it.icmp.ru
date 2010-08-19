<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "новые",
    "unreadlist",
    "message",
    null,
    new { title = "Полученные не прочитанные сообщения" }
)%>
