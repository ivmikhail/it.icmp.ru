<%@ Control Language="C#" Inherits="ViewUserControl<Header>" %>


<%= Html.ActionLink(
    "блокировать",
    "block",
    "header",
    new { id = Model.Id },
    new { title = "Остановить показ хидера и блокировать пользователя на добавление хидера" }
)%>
