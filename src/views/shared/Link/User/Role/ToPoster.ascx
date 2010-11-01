<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "постер",
    "changerole",
    "user",
    new { nick = Model.Nick, role = "poster" },
    new { title = "Сделать постером, в смысле добавляющим посты" }
)%>