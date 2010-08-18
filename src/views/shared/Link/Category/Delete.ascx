<%@ Control Language="C#" Inherits="ViewUserControl<Category>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "category",
    new { id = Model.Id },
    new { title = "Удалить категорию поста", @class = "delete-link" }
)%>
