<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "посты",
    "list",
    "post",
    null,
    new { title = "Посмотреть посты" }
 )%>
