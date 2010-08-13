<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "написать",
    "add",
    "post",
    null,
    new { title = "Написать пост" }
 )%>
