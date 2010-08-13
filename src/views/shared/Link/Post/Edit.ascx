<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    "редактировать",
    "edit",
    "post",
    new { id = Model.Id },
    new { title = "Редактировать пост" }
 )%>
