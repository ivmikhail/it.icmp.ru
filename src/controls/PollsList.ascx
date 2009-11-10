<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollsList.ascx.cs" Inherits="ITCommunity.PollsList"%>

<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>

<asp:Repeater ID="RepeaterPolls" runat="server" >
	<HeaderTemplate>
		<table id="polls-table" class="data-table">
			<caption>Архив опросов</caption>
			<thead>
				<th>Вопрос</th>
				<th>Проголосовавших</th>
				<th>Дата начала</th>
				<th>Дата окончания</th>
				<th></th>
			</thead>
			<tbody>
	</HeaderTemplate>
	<ItemTemplate>
		<tr class="even">
			<td><%# Eval("topic")%></td>
			<td><%# Eval("votescount") %></td>
			<td><%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%></td>
			<td><%# Eval("enddatestring")%></td>
			<td><a href="pollresult.aspx?id=<%# Eval("id") %>" title="Посмотреть результаты">результаты</a></td>
		</tr> 
	</ItemTemplate>
	<AlternatingItemTemplate>
		<tr class="odd">
			<td><%# Eval("topic")%></td>
			<td><%# Eval("votescount") %></td>
			<td><%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%></td>
			<td><%# Eval("enddatestring")%></td>
			<td><a href="pollresult.aspx?id=<%# Eval("id") %>" title="Посмотреть результаты">результаты</a></td>
		</tr> 
	</AlternatingItemTemplate>
	<FooterTemplate>
			</tbody>
		</table>
	</FooterTemplate>
</asp:Repeater>

<uc:Pager id="Pager" runat="server" />
