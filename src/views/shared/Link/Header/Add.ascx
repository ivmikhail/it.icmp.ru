<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "написать хидер",
    "add",
    "header",
    null,
    new { title = "Написать текст показываемый на хидере" }
)%>
