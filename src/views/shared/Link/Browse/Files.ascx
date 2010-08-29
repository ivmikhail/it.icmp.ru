<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "файлы",
    "view",
    "browse",
    new { },
    new { title = "Файлы" }
)%>
