<%@ Control Language="C#" Inherits="ViewUserControl<PostEditCategoriesModel>" %>


<ul class="select-categories">
    <% foreach (var category in Categories.All) { %>
        <li>
            <%= Ajax.ActionLink(
                category.Name,
                "togglecategory",
                "post",
                new { categoryId = category.Id },
                new AjaxOptions { UpdateTargetId = "select-categories" },
                new {
                    @class = (Model.IsAttached[category.Id]) ? "attached-category" : "disattached-category",
                    title = (Model.IsAttached[category.Id]) ? "Убрать категорию" : "Добавить категорию"
                }
            )%>
        </li>
    <% } %>
</ul>