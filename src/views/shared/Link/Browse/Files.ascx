<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "файлы",
    "view",
    "browse",
    new { directory = "/Files/" },
    new { title = "Файлы" }
)%>
