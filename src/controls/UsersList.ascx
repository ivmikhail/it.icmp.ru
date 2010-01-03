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
		<tr class="even">
			<td><%# Eval("login") %></td>
			<td><%# Eval("email") %></td>
		</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
		<tr class="odd">
			<td><%# Eval("login") %></td>
			<td><%# Eval("email") %></td>
		</tr>
	</AlternatingItemTemplate>
	<FooterTemplate>
		</tbody>
	</table>
	</FooterTemplate>
</asp:Repeater>
