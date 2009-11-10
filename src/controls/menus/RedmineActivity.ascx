<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RedmineActivity.ascx.cs" Inherits="ITCommunity.RedmineActivity" %>

<div class="menu-panel">
	<h1>Активность Redmine</h1>

	<asp:Repeater ID="ActivityItems" runat="server" EnableViewState="false">
		<ItemTemplate>
			<%# Eval("Author") %> - 
			<a href='<%# Eval("Url") %>' target="_blank"><%# Eval("Title") %></a>
			<br />
		</ItemTemplate>
	</asp:Repeater>
</div>