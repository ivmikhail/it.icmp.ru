<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "капчи",
    "list",
    "captcha",
    null,
    new { title = "Редактировать IT капчи" }
 )%>
