<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "админы",
    "list",
    "user",
    new { role = "admin" },
    new { title = "Показать только админов" }
 )%>
