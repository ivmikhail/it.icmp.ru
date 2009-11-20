<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="headertexts.aspx.cs" Inherits="ITCommunity.HeaderTexts" Title="Ykt IT Community | Тексты хидера" %>

<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:Panel ID="CantEditAdminError" runat="server" CssClass="error" Visible="false">
		Нельзя заблокировать возможность добавления текста для хидера админа
	</asp:Panel>

	<asp:Repeater ID="HeaderTextsRepeater" runat="server">
		<HeaderTemplate>
			<table class="data-table">
				<caption>Тексты для хидера</caption>
				<thead>
					<tr>
						<th>Пользователь</th>
						<th>Текст</th>
						<th>Создано</th>
						<th>Начало показа</th>
						<th>Конец показа</th>
						<th></th>
						<th></th>
					</tr>
				</thead>
				
		</HeaderTemplate>
		<ItemTemplate>
			<tr class="even">
				<td><%# Eval("User.Nick") %></td>
				<td><%# Eval("Text") %></td>
				<td><%# Eval("CreateDate", "{0:dd MMMM yyyy, HH:mm}")%></td>
				<td><%# Eval("ShowBeginDate").Equals(DateTime.MinValue) ? "" : Eval("ShowBeginDate", "{0:dd MMMM yyyy, HH:mm}")%></td>
				<td><%# Eval("ShowEndDate").Equals(DateTime.MinValue) ? "" : Eval("ShowEndDate", "{0:dd MMMM yyyy, HH:mm}")%></td>
				<td><a href="headertexts.aspx?del=<%# Eval("Id") %>" title="Только удалить запись">удалить</a></td>
				<td><a href="headertexts.aspx?del=<%# Eval("Id") %>&amp;block=<%# Eval("User.Id") %>" title="Удалить эту запись и добавить пользователя в черный список.">заблокировать</a></td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr class="odd">
				<td><%# Eval("User.Nick")%></td>
				<td><%# Eval("Text") %></td>
				<td><%# Eval("CreateDate", "{0:dd MMMM yyyy, HH:mm}")%></td>
				<td><%# Eval("ShowBeginDate").Equals(DateTime.MinValue) ? "" : Eval("ShowBeginDate", "{0:dd MMMM yyyy, HH:mm}")%></td>
				<td><%# Eval("ShowEndDate").Equals(DateTime.MinValue) ? "" : Eval("ShowEndDate", "{0:dd MMMM yyyy, HH:mm}")%></td>
				<td><a href="headertexts.aspx?del=<%# Eval("Id") %>" title="Только удалить запись">удалить</a></td>
				<td><a href="headertexts.aspx?del=<%# Eval("Id") %>&amp;block=<%# Eval("User.Id") %>" title="Удалить эту запись и добавить пользователя в черный список.">заблокировать</a></td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>

	<asp:Repeater ID="BlockedUsersRepeater" runat="server">
		<HeaderTemplate>
			<table class="data-table">
				<caption>Заблокированные пользователи</caption>
				<thead>
					<tr>
						<th>Пользователь</th>
						<th></th>
						<th></th>
					</tr>
				</thead>
				
		</HeaderTemplate>
		<ItemTemplate>
			<tr class="even">
				<td><%# Eval("Nick") %></td>
				<td><a href="headertexts.aspx?unblock=<%# Eval("Id") %>" title="Дать возможность добавлять текст для хидера">разблокировать</a></td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr class="odd">
				<td><%# Eval("Nick") %></td>
				<td><a href="headertexts.aspx?unblock=<%# Eval("Id") %>" title="Дать возможность добавлять текст для хидера">разблокировать</a></td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater>

	<uc:Pager id="HeaderTextsPager" runat="server" />
</asp:Content>
