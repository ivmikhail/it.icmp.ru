<%@ Control Language="C#" Inherits="ViewUserControl<Rss>" %>


<a 
    href="<%= Model.Uri %>" title="Прямая ссылка на rss">
    <img alt="RSS" src="<%= Url.Content("~/content/img/design/rss.png")%>" class="middle" /></a>