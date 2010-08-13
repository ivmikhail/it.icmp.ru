<%@ Control Language="C#" Inherits="ViewUserControl<Comment>" %>


 <%= Html.ActionLink(
     "редактировать",
     "edit",
     "comment",
     new { id = Model.Id },
     new { title = "Редактируется только последние 5 минут после добавления" }
)%>
