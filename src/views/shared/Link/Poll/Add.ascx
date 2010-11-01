<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "добавить опрос",
    "add",
    "poll",
    null,
    new { title = "Добавить опрос" }
)%>
