<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "избранные",
    "posts",
    "favorite",
    null,
    new { title = "Посмотреть избранные посты" }
)%>
