<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "забанденные",
    "list",
    "user",
    new { role = "banned" },
    new { title = "Показать только забаненных" }
 )%>
