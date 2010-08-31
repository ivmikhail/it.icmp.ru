<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


<%= Html.ActionLink(
    "редактировать",
    "editpoll",
    "post",
    new { id = Model.Id },
    new { title = "Редактировать опрос" }
)%>
