<%@ Control Language="C#" Inherits="ViewUserControl<Captcha>" %>


<a  href="<%= Url.Action("delete", "captcha", new { id = Model.Id }) %>"
    title = "Удалить капчу"
    class = "delete-link">
    удалить</a>
