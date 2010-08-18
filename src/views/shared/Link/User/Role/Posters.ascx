<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "простеры",
    "list",
    "user",
    new { role = "poster" },
    new { title = "Показать только постеров" }
 )%>
