<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "прочитанные",
    "reads",
    "message",
    null,
    new { title = "Полученные прочитанные сообщения" }
)%>
