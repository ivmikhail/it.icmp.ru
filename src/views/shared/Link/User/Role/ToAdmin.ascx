<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "админ",
    "changerole",
    "user",
    new { nick = Model.Nick, role = "admin" },
    new { title = "Сделать админом" }
)%>