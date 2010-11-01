<%@ Control Language="C#" Inherits="ViewUserControl<Header>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "header",
    new { id = Model.Id },
    new { title = "Удалить текст хидера", @class="delete-link" }
)%>
