<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "войти",
    "login",
    "user",
    null,
    new { title = "Приветствуем!" }
)%>