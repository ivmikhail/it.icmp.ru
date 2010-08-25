<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "Обсуждаемые",
    "discussiblelist",
    "post",
    new { period = "month" },
    new { title = "Посмотреть обсуждаемые посты" + Config.Get("DiscussiblePostsTimeText") }
)%>
