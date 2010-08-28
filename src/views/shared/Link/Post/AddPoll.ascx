<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "написать опрос",
    "addpoll",
    "post",
    null,
    new { title = "Написать опрос" }
)%>
