<%@ Control Language="C#" Inherits="ViewUserControl<Captcha>" %>


<%= Html.ActionLink(
    "удалить",
    "delete",
    "captcha",
    new { id = Model.Id },
    new { title = "Удалить капчу", @class = "delete-link" }
)%>
