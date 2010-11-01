<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "комментарии",
    "comments",
    "user",
    new { nick = Model.Nick },
    new { title = "Комментарии - " + Model.Nick }
)%>