﻿<%@ Control Language="C#" Inherits="ViewUserControl<dynamic>" %>


<% if (Config.SiteAddress == "http://it.icmp.ru") { %>
    <div class="banner">
        <!--/* OpenX Javascript Tag v2.8.5 */-->
        <script type='text/javascript'><!--        //<![CDATA[
            var m3_u = (location.protocol == 'https:' ? 'https://it.icmp.ru/openx/www/delivery/ajs.php' : 'http://it.icmp.ru/openx/www/delivery/ajs.php');
            var m3_r = Math.floor(Math.random() * 99999999999);
            if (!document.MAX_used) document.MAX_used = ',';
            document.write("<scr" + "ipt type='text/javascript' src='" + m3_u);
            document.write("?zoneid=1");
            document.write('&amp;cb=' + m3_r);
            if (document.MAX_used != ',') document.write("&amp;exclude=" + document.MAX_used);
            document.write(document.charset ? '&amp;charset=' + document.charset : (document.characterSet ? '&amp;charset=' + document.characterSet : ''));
            document.write("&amp;loc=" + escape(window.location));
            if (document.referrer) document.write("&amp;referer=" + escape(document.referrer));
            if (document.context) document.write("&context=" + escape(document.context));
            if (document.mmm_fo) document.write("&amp;mmm_fo=1");
            document.write("'><\/scr" + "ipt>");
        //]]>--></script>
        <noscript>
            <div>
                <a href='http://it.icmp.ru/openx/www/delivery/ck.php?n=a9a7f1b8&amp;cb=INSERT_RANDOM_NUMBER_HERE'>
                    <img src='http://it.icmp.ru/openx/www/delivery/avw.php?zoneid=1&amp;cb=INSERT_RANDOM_NUMBER_HERE&amp;n=a9a7f1b8' alt='' />
                </a>
            </div>
        </noscript>
    </div>
<% } %>