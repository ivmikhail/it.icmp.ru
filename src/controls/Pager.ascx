<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="ITCommunity.Pager" %>

<asp:Panel ID="PagerPanel" runat="server">

	<script type="text/javascript" src="media/js/float-pager.js" charset="utf-8"></script>

	<ul id="pager">
		<asp:Literal ID="PreviousPageText" runat="server" />

		<asp:Repeater ID="BeforePages" runat="server">
			<ItemTemplate>
				<li>
					<a href="<%# PagePath %>?<%# QueryString %><%# QueryName %>=<%# Container.DataItem %>" title="Страница <%# Container.DataItem %>"><%# Container.DataItem %></a>
				</li>
			</ItemTemplate>
		</asp:Repeater>

		<li class="active"><asp:Literal ID="CurrentPageText" runat="server" /></li>

		<asp:Repeater ID="AfterPages" runat="server">
			<ItemTemplate>
				<li>
					<a href="<%# PagePath %>?<%# QueryString %><%# QueryName %>=<%# Container.DataItem %>" title="Страница <%# Container.DataItem %>"><%# Container.DataItem %></a>
				</li>
			</ItemTemplate>
		</asp:Repeater>

		<asp:Literal ID="NextPageText" runat="server" />
	</ul>

</asp:Panel>
