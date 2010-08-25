<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    CurrentUser.User.Nick,
    "profile",
    "user",
    null,
    new { title = "Мой профиль" }
)%>
