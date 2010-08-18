<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "post",
    new { id = Model.Id },
    new { title = "Удалять пост опасно!", @class = "delete-link" }
)%>
