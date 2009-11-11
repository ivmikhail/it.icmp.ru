<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RedmineActivity.ascx.cs" Inherits="ITCommunity.RedmineActivity" %>

<div class="menu-panel">
	<h1>Активность Redmine</h1>

	<ul id="redmine-activity">
		<asp:Repeater ID="ActivityItems" runat="server" EnableViewState="false">
			<ItemTemplate>
				<li>
					<b><%# Eval("Author") %></b> - 
					<a href='<%# Eval("Url") %>' target="_blank"><%# Eval("Title") %></a>
				</li>
			</ItemTemplate>
		</asp:Repeater>
	</ul>
</div>
