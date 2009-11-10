<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinksMenu.ascx.cs" Inherits="ITCommunity.LinksMenu" %>

<div id="links-menu" class="menu-panel">
	<h1>Меню</h1>
	<asp:Repeater ID="RepeaterMenu" runat="server" OnItemDataBound="RepeaterMenu_ItemDataBound" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li class="sub-menu">
				<h3><%# Eval("name")%></h3>

				<asp:Repeater ID="RepeaterSubMenu" runat="server">
					<HeaderTemplate>
						<ul>
					</HeaderTemplate>
					<ItemTemplate>
						<li>
							<a href="<%# Eval("url")%>" target="<%# IsBlank(Container.DataItem)%>"><%# Eval("name")%></a>
						</li>
					</ItemTemplate>
					<FooterTemplate>
						</ul>
					</FooterTemplate>
				</asp:Repeater>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
