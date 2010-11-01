<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<%= Html.ActionLink(
    "rss-ки",
    "list",
    "rss",
    null,
    new { title = "Загружаемые rss-ки" }
 )%>
