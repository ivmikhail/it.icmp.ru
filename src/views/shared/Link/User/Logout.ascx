<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "выйти",
    "logout",
    "user",
    null,
    new { title = "Зачем выходить?" }
)%>