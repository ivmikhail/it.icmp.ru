<%@ Control Language="C#" Inherits="ViewUserControl<Header>" %>


<%= Html.ActionLink(
    "остановить",
    "stop",
    "header",
    new { id = Model.Id },
    new { title = "Остановить показ хидера" }
)%>
