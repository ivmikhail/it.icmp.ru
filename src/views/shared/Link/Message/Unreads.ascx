<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "новые",
    "unreads",
    "message",
    null,
    new { title = "Полученные не прочитанные сообщения" }
)%>
