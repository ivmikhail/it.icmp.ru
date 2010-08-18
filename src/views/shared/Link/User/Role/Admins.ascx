<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "одмины",
    "list",
    "user",
    new { role = "admin" },
    new { title = "Показать только админов" }
 )%>
