<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "войти",
    "login",
    "user",
    new { returnUrl = Request.Url.AbsolutePath },
    new { title = "Приветствуем!" }
)%>
