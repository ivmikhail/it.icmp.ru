<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "меню",
    "list",
    "menuitem",
    null,
    new { title = "Редактировать меню расположенный в подвале" }
 )%>
