<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "файлы",
    "files",
    "browser",
    null,
    new { title = "Файлы" }
)%>
