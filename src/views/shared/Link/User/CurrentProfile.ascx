<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    CurrentUser.User.Nick,
    "profile",
    "user",
    new { nick = CurrentUser.User.Nick },
    new { title = "Мой профиль" }
)%>
