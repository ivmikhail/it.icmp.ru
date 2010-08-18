<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "забанен",
    "changerole",
    "user",
    new { nick = Model.Nick, role = "banned" },
    new { title = "Забанить аккаунт пользователя" }
)%>