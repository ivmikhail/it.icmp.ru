<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "зарегистрироваться",
    "register",
    "user",
    null,
    new { title = "Присоединяйтесь!" }
)%>
