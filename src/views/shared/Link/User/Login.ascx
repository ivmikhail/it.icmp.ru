<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "войти",
    "login",
    "user",
    new { returnUrl = (Request.Url.AbsolutePath != Url.Action("login", "user")) ? Request.Url.AbsolutePath : Request["returnUrl"] },
    new { title = "Приветствуем!" }
)%>
