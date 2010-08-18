<%@ Control Language="C#" Inherits="ViewUserControl<Captcha>" %>


<%= Html.ActionLink(
    "редактировать",
    "edit",
    "captcha",
    new { id = Model.Id },
    new { title = "Редактировать капчу" }
)%>
