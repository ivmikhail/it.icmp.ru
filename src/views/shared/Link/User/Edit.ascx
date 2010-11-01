<%@ Control Language="C#" Inherits="ViewUserControl<User>" %>


<%= Html.ActionLink(
    "редактировать",
    "edit",
    "user",
    null,
    new { title = "Редактировать свои данные" }
)%>