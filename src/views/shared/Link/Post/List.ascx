<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "все посты",
    "list",
    "post",
    null,
    new { title = "Посмотреть все посты" }
 )%>
