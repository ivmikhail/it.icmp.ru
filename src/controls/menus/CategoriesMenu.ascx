<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoriesMenu.ascx.cs" Inherits="ITCommunity.CategoriesMenu" %>

<div id="categories-menu" class="menu-panel">
	<h1>Категории</h1>
	<asp:Repeater ID="NewsCategories" runat="server" >
		<HeaderTemplate>
			<ul>
		</HeaderTemplate>
		<ItemTemplate>
			<li>
				<a href='default.aspx?cat=<%# Eval("id")%>' title="Смотреть посты только этой категории" class="category-link"><%# Eval("name")%></a>
			</li>
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
