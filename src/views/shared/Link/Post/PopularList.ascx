<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "Популярные",
    "popularlist",
    "post",
    new { period = "month" },
    new { title = "Посмотреть популярные посты" + Config.Get("PopularPostsTimeText") }
)%>
