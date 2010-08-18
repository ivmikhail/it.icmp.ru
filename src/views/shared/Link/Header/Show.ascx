<%@ Control Language="C#" Inherits="ViewUserControl<Header>" %>


<%= Html.ActionLink(
    "показать",
    "show",
    "header",
    new { id = Model.Id },
    new { title = "Начать показывать заново" }
)%>
