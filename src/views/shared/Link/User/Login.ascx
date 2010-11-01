<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "войти",
    "login",
    "user",
    new { returnUrl = (Request.Url.LocalPath == Url.Action("login", "user")) ? Request["returnUrl"]  : Request.Url.AbsolutePath},
    new { title = "Приветствуем!" }
)%>
