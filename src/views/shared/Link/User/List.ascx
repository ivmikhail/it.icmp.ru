<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "юзеры",
    "list",
    "user",
    new { role = "all" },
    new { title = "Редактировать роли юзеров" }
 )%>
