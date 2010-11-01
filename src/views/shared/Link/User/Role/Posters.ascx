<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "постеры",
    "list",
    "user",
    new { role = "poster" },
    new { title = "Показать только постеров" }
 )%>
