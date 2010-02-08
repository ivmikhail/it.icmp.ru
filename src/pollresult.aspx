<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pollresult.aspx.cs" Inherits="ITCommunity.PollResultPage" Title="Ykt IT Community | Результаты опросы" %>

<%@ Register Assembly="OpenFlashChartLib" Namespace="OpenFlashChartLib.Controls" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<asp:Literal ID="PollMessageText" runat="server" />

	<uc:OpenFlashChartControl ID="OpenFlashChartControl" runat="server" FlashUrl="media/other/open-flash-chart.swf" OnDrawChart="DrawChart" />
	<div>
		<asp:Literal ID="NoMultiSelectText" runat="server">
			Можно выбрать только один вариант
		</asp:Literal>
		<asp:Literal ID="MultiSelectText" runat="server" Visible="false">
			Можно выбрать несколько вариантов
		</asp:Literal>
		, всего проголосовало - <asp:Literal ID="VotersCountText" runat="server" />

		<asp:LinkButton ID="DeletePollLink" runat="server" Visible="false" ToolTip="Удалить этот опрос" OnClick="DeletePollLink_Click" OnClientClick="return confirm('Точно удалить?')">
			Удалить опрос
		</asp:LinkButton>
	</div>

	<div>
		<h2>Период голосования</h2>
		<asp:Literal ID="CreateDateText" runat="server" />
		-
		<asp:Literal ID="CloseDateText" runat="server" />
	</div>

	<div>
		<h2>Кто как голосовал</h2>

		<asp:Literal ID="ClosedPollText" runat="server" Visible="false">
			Голосование закрытое, данные не доступны.
		</asp:Literal>

		<asp:Repeater ID="Answers" runat="server" OnItemDataBound="Answers_ItemDataBound">
			<ItemTemplate>
				<h3><%# Eval("Text") %></h3>
				<asp:Repeater ID="Voters" runat="server">
					<ItemTemplate>
						<a href="user.aspx?login=<%# Eval("Login") %>" title="Посетить страницу пользователя" class="user-pm-link"><%# Eval("Login") %></a>
					</ItemTemplate>
				</asp:Repeater>
			</ItemTemplate>
		</asp:Repeater>
	</div>
</asp:Content>
