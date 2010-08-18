<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "обсуждаемые",
    "discussibles",
    "post",
    new { period = "week" },
    new { title = "Посмотреть обсуждаемые за последнюю неделю посты" }
)%>
