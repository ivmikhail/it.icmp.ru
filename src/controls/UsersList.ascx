<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UsersList.ascx.cs" Inherits="ITCommunity.UsersList" %>
<asp:Repeater ID="RepeaterUsers" runat="server">
	<HeaderTemplate>
		<table class="data-table">
			<thead>
				<th>login</th>
				<th>email</th>
			</thead>
			<tbody>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td><%# Eval("nick") %></td>
			<td><%# Eval("email") %></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		</tbody>
	</table>
	</FooterTemplate>
</asp:Repeater>
