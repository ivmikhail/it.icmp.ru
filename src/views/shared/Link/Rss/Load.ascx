<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "другие rss-ки",
    "load",
    "rss",
    null,
    new { title = "Посмотреть rss-ки других сайтов" }
 )%>
