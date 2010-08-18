<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "написать пост",
    "add",
    "post",
    null,
    new { title = "Написать пост" }
)%>
