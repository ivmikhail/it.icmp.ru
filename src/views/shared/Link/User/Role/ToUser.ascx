<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "юзер",
    "changerole",
    "user",
    new { nick = Model.Nick, role = "user" },
    new { title = "Сделать юзером" }
)%>