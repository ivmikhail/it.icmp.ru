<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "категории",
    "list",
    "category",
    null,
    new { title = "Редактировать категории постов" }
 )%>
