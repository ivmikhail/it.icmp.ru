<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    CurrentUser.User.Nick,
    "view",
    "user",
    new { nick = CurrentUser.User.Nick },
    new { title = "Мое" }
)%>
