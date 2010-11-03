<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% if (Request.Url.AbsoluteUri == Config.SiteAddress + "/" && Config.SiteAddress == "http://it.icmp.ru") {%>
    <a href="http://www.ykt.ru/yaknet/default.asp" title="Перейти в рейтинговую систему">
        <img src="http://www.ykt.ru/yaknet/image.asp?id=IT_community" alt="Рейтинг Ykt.Ru" /></a>
<% } %>