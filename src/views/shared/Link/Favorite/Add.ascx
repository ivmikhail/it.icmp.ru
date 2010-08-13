<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "добавить",
    "add",
    "favorite",
    new { id = Model.Id },
    new { title = "Добавить в избранные" }
)%>
