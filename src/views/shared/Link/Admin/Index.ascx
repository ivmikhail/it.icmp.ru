<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "админка",
    "list",
    "header",
    null,
    new { title = "Сделать что-то важное" }
)%>
