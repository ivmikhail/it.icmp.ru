<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "прочитанные",
    "readlist",
    "message",
    null,
    new { title = "Полученные прочитанные сообщения" }
)%>
