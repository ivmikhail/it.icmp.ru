<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "есчо",
    "discussible",
    "posts",
    null,
    new { title = "Посмотреть больше", @class = "more-link" }
)%>