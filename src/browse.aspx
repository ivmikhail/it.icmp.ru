<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="browse.aspx.cs" Inherits="ITCommunity.Browse" Title="Ykt IT Community | Файлы" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<table id="files-table" class="data-table">
		<caption>
		<asp:Repeater ID="rptPath" runat="Server" >	
		<ItemTemplate>/<a href='<%# Eval("Link") %>' title="Перейти в директорию"><%# Eval("Name") %></a></ItemTemplate>
		</asp:Repeater>
		</caption>
		<thead>
			<tr>
				<th></th>
				<th>Название</th>
				<th>Размер</th>
			</tr>
		</thead>
		<tbody>
			<asp:Repeater ID="rptFiles" runat="Server" >
				<ItemTemplate>
					<tr class="even">
						<td class="file-icon"><img src='media/img/browser/<%# Eval("Icon") %>' alt='<%# Eval("Name") %>' /></td>
						<td><a href='<%# Eval("Link") %>' title='<%# Eval("Description") %>'><%# Eval("Name") %></a></td>
						<td><%# Eval("Size") %></td>
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr class="odd">
						<td class="file-icon"><img src='media/img/browser/<%# Eval("Icon") %>' alt='<%# Eval("Name") %>' /></td>
						<td><a href='<%# Eval("Link") %>' title='<%# Eval("Description") %>'><%# Eval("Name") %></a></td>
						<td><%# Eval("Size") %></td>
					</tr>
				</AlternatingItemTemplate>
			</asp:Repeater>
		</tbody>
	</table>
</asp:Content>

