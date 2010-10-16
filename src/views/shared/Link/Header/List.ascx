<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "хидеры",
    "list",
    "header",
    null,
    new { title = "Редактировать тексты показываемые в хидере" }
 )%>
