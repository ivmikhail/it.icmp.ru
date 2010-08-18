<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "юзверы",
    "list",
    "user",
    new { role = "user" },
    new { title = "Показать только юзеров" }
 )%>
