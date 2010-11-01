<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "юзеры",
    "list",
    "user",
    new { role = "user" },
    new { title = "Показать только юзеров" }
 )%>
