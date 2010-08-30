<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "добавить опрос",
    "addpoll",
    "post",
    null,
    new { title = "Добавить опрос" }
)%>
