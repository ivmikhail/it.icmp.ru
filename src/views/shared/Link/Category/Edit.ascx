<%@ Control Language="C#" Inherits="ViewUserControl<Category>" %>


<%= Html.ActionLink(
    "редактировать",
    "list",
    "category",
    new { id = Model.Id },
    new { title = "Редактировать категорию поста" }
)%>
