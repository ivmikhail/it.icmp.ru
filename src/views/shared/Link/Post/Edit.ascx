<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    "редактировать",
    "edit",
    Model.EntityType.ToString().ToLower(),
    new { id = Model.Id },
    new { title = "Редактировать пост" }
)%>
