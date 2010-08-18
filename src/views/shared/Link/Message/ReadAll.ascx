<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "все прочитано",
    "readall",
    "message",
    null,
    new { title = "Отметить все новые сообщения как прочитанные" }
)%>
