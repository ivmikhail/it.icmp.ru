<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "популярные",
    "popularlist",
    "post",
    new { period = "week" },
    new { title = "Посмотреть популярные за последнюю неделю посты" }
)%>
