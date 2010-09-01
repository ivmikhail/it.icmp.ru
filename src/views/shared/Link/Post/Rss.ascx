<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<a 
    href="<%= Url.Action("feed", "rss") %>"
    title="Лента новостей в формате RSS 2.0">
    <img alt="RSS" src="<%= Url.Content("~/content/img/design/rss.png")%>" class="middle" /></a> 
