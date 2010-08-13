<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    Model.Nick,
    "profile",
    "user",
    new { nick = Model.Nick },
    new { @class = "user-link", title = "Посетить страницу пользователя" }
)%>
