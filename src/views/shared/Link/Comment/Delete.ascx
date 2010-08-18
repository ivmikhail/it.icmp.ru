<%@ Control Language="C#" Inherits="ViewUserControl<Comment>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "comment",
    new { id = Model.Id },
    new { title = "Удалить комментарий", @class = "delete-link" }
)%>
