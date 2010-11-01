<%@ Control Language="C#" Inherits="ViewUserControl<Category>" %>


<%= Html.ActionLink(
    Model.Name,
    "posts",
    "category",
    new { id = Model.Id },
    new { title = "Всего " + Model.PostsCount + " постов", @class="category-link" }
)%>