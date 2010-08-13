<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "есчо",
    "popular",
    "posts",
    null,
    new { title = "Посмотреть больше", @class = "more-link" }
)%>