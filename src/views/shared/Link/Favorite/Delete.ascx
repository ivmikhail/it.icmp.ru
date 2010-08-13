<%@ Control Language="C#" Inherits="ViewUserControl<Post>" %>


 <%= Html.ActionLink(
    "убрать",
    "delete",
    "favorite",
    new { id = Model.Id },
    new { title = "Убрать из избранных" }
)%>