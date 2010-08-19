<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    "избранное",
    "add",
    "favorite",
    new { id = Model.Id },
    new { title = "Добавить в избранные" }
)%>
