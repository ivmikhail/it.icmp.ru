<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopPostersMenu.ascx.cs" Inherits="ITCommunity.TopPostersMenu" %>

<div id="top-posters-menu" class="menu-panel">
	<h1>Активные постеры</h1>
	<asp:Repeater ID="TopPosters" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href="mailsend.aspx?receiver=<%# Eval("key") %>" title="Отправить личное сообщение" class="user-pm-link"><%# Eval("key")%></a> (<%# Eval("value")%>)
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
