<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "посты",
    "posts",
    "user",
    new { nick = Model.Nick },
    new { title = "Посмотреть посты - " + Model.Nick }
)%>