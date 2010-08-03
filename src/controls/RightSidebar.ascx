<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RightSidebar.ascx.cs" Inherits="ITCommunity.RightSidebar" %>

<%@ Register Src="~/controls/menus/LastCommentsMenu.ascx" TagName="LastCommentsMenu" TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/PollMenu.ascx"         TagName="PollMenu"         TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/PopularPostsMenu.ascx" TagName="PopularPostsMenu" TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/SearchMenu.ascx"       TagName="SearchMenu"       TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/TopPostersMenu.ascx"   TagName="TopPostersMenu"   TagPrefix="uc" %>
<%@ Register Src="~/controls/menus/UsersStatsMenu.ascx"   TagName="UsersStatsMenu"   TagPrefix="uc" %>

<div id="right-sidebar">
	<div class="content">
	
<!--/* OpenX Javascript Tag v2.8.5 */-->

<script type='text/javascript'><!--//<![CDATA[
   var m3_u = (location.protocol=='https:'?'https://it.icmp.ru/openx/www/delivery/ajs.php':'http://it.icmp.ru/openx/www/delivery/ajs.php');
   var m3_r = Math.floor(Math.random()*99999999999);
   if (!document.MAX_used) document.MAX_used = ',';
   document.write ("<scr"+"ipt type='text/javascript' src='"+m3_u);
   document.write ("?zoneid=1");
   document.write ('&amp;cb=' + m3_r);
   if (document.MAX_used != ',') document.write ("&amp;exclude=" + document.MAX_used);
   document.write (document.charset ? '&amp;charset='+document.charset : (document.characterSet ? '&amp;charset='+document.characterSet : ''));
   document.write ("&amp;loc=" + escape(window.location));
   if (document.referrer) document.write ("&amp;referer=" + escape(document.referrer));
   if (document.context) document.write ("&context=" + escape(document.context));
   if (document.mmm_fo) document.write ("&amp;mmm_fo=1");
   document.write ("'><\/scr"+"ipt>");
//]]>--></script><noscript><a href='http://it.icmp.ru/openx/www/delivery/ck.php?n=a9a7f1b8&amp;cb=INSERT_RANDOM_NUMBER_HERE' target='_blank'><img src='http://it.icmp.ru/openx/www/delivery/avw.php?zoneid=1&amp;cb=INSERT_RANDOM_NUMBER_HERE&amp;n=a9a7f1b8' border='0' alt='' /></a></noscript>

		<uc:SearchMenu ID="SearchMenu" runat="server" />

		<uc:PollMenu ID="PollMenu" runat="server" />

		<uc:LastCommentsMenu ID="LastCommentsMenu" runat="server" />
		
		<uc:PopularPostsMenu ID="PopularPostsMenu" runat="server" />

		<uc:TopPostersMenu ID="TopPostersMenu" runat="server" />

		<uc:UsersStatsMenu ID="UsersStatsMenu" runat="server" />

	</div>
</div>
