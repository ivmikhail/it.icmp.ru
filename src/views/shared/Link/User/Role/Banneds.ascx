<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "забаненные",
    "list",
    "user",
    new { role = "banned" },
    new { title = "Показать только забаненных" }
 )%>
