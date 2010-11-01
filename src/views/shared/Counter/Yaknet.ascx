<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% if (Request.Url.LocalPath == "/" && Config.SiteAddress == "http://it.icmp.ru") { %>
    <a href="http://www.ykt.ru/yaknet/default.asp" title="Перейти в рейтинговую систему" target="_blank" class="counter">
        <img src="http://www.ykt.ru/yaknet/image.asp?id=IT_community" alt="Рейтинг Ykt.Ru" border="0" width="50" height="30" /></a>
<% } %>