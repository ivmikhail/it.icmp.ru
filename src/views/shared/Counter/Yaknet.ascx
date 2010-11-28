<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% 
    var url = Request.Url.ToString().ToLower();
    if (url == "http://it.icmp.ru"  ||
        url == "http://it.icmp.ru/" ||
        url == "http://it.icmp.ru/?page=1") {%>        
    <a href="http://www.ykt.ru/yaknet/default.asp" title="Перейти в рейтинговую систему">
        <img src="http://www.ykt.ru/yaknet/image.asp?id=IT_community" alt="Рейтинг Ykt.Ru" /></a>
<% } %>