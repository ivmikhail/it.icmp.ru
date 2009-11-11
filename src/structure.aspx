<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="structure.aspx.cs" Inherits="ITCommunity.StructurePage" Title="Ykt IT Community | Управлять категориями/меню" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<script type="text/javascript">
		window.addEvent('domready', init);
		function init() {
			myTabs = new TabSwapper({
					selectedClass: 'on',
					deselectedClass: 'off',
					tabs: $$('.tabs .clickers li'),
					clickers: $$('.tabs .clickers a'),
					sections: $$('.tabs .sections .section'),
					cookieName: 'structure-tabs',
					smooth: true,
					smoothSize: true
				});
			}
	</script>

	<h1>Список категорий/меню</h1>
	<div class="note">
		Внимание после каких-либо изменений, рекомендуется 
		<asp:LinkButton ID="LinkButtonDropCache" runat="server" OnClick="LinkButtonDropCache_Click">сбросить кеш</asp:LinkButton>
	</div>

	<asp:Literal ID="ResetCacheText" runat="server" Visible="false">
		<div class="message">
			Кеш категорий и меню сброшен
		</div>
	</asp:Literal>

	<div id="structure-tabs" class="tabs">
		<ul class="clickers">
			<li class="off"><a>Категории</a></li>
			<li class="off"><a>Меню</a></li>
		</ul>

		<ul class="sections">
			<li class="section">
				<h2>Добавить категорию</h2>

				<label class="textbox-input">
					<span class="label-title">Название</span>
					<asp:TextBox ID="TextBoxCatName" runat="server" />
				</label>

				<label class="textbox-input">
					<span class="label-title">Сортировка (int)</span>
					<asp:TextBox ID="TextBoxCatSort" runat="server" />
				</label>

				<div class="big-button">
					<asp:LinkButton ID="LinkButtonAddCat" runat="server" OnClick="LinkButtonAddCat_Click">добавить</asp:LinkButton>
				</div>

				<asp:GridView ID="gv_categories" runat="server" AllowSorting="True" AutoGenerateColumns="False"
						DataKeyNames="Id" DataSourceID="SqlDataSourceCategories" PageSize="20" Caption="Список категорий"
						EnableSortingAndPagingCallbacks="True" CssClass="data-table" GridLines="None">
					<Columns>
						<asp:BoundField DataField="id"   HeaderText="id"   SortExpression="id" ReadOnly="True"/>
						<asp:BoundField DataField="name" HeaderText="name" SortExpression="name"/>
						<asp:BoundField DataField="sort" HeaderText="sort(int)" SortExpression="sort"/>
						<asp:CommandField ShowEditButton="True" ShowDeleteButton="True" EditText="edit" CancelText="cancel" DeleteText="del" UpdateText="upd" />
					</Columns>
					<AlternatingRowStyle CssClass="odd" />
				</asp:GridView>
				<asp:SqlDataSource ID="SqlDataSourceCategories" runat="server" ConnectionString="<% =Global.ConnectionString() %>"
						ProviderName="<%$ ConnectionStrings:ConnectionString.providerName%>"
						SelectCommandType="StoredProcedure" SelectCommand="CategoryGetAll"
						DeleteCommandType="StoredProcedure" DeleteCommand="CategoryDel"
						UpdateCommandType="StoredProcedure" UpdateCommand="CategoryUpdate">
					<DeleteParameters>
						<asp:Parameter Name="id" Type="Int32"  Direction="Input" DefaultValue="-1"/>
					</DeleteParameters>
					<UpdateParameters>
						<asp:Parameter Name="id"   Type="Int32"  Direction="Input" DefaultValue="-1" />
						<asp:Parameter Name="name" Type="String" Direction="Input" DefaultValue="null"/>
						<asp:Parameter Name="sort" Type="Int32"  Direction="Input" DefaultValue="0" />
					</UpdateParameters>
				</asp:SqlDataSource>
			</li>

			<li class="section">
				<h2>Добавить пункт в меню</h2>

				<label class="textbox-input">
					<span class="label-title">Название</span>
					<asp:TextBox ID="TextBoxMenuName" runat="server" />
				</label>

				<label class="textbox-input">
					<span class="label-title">Parent Id (int)</span>
					<asp:TextBox ID="TextBoxMenuParent" runat="server" />
				</label>

				<label class="checkbox-input">
					<span class="label-title">
						Открывать в новом окне
						<asp:CheckBox ID="IsBlankCheckBox" runat="server" />
					</span>
				</label>

				<label class="textbox-input">
					<span class="label-title">Url</span>
					<asp:TextBox ID="TextBoxMenuUrl" runat="server" />
				</label>

				<label class="textbox-input">
					<span class="label-title">Сортировка (int)</span>
					<asp:TextBox ID="TextBoxMenuSort" runat="server"/>
				</label>

				<div class="big-button">
					<asp:LinkButton ID="LinkButtonAddMenu" runat="server" OnClick="LinkButtonAddMenu_Click">Добавить</asp:LinkButton>
				</div>

				<asp:GridView ID="gv_menuitems" runat="server" AllowSorting="True" AutoGenerateColumns="False"
						DataKeyNames="id" DataSourceID="SqlDataSourceMenu" PageSize="20" Caption="Список ссылок менюшки"
						EnableSortingAndPagingCallbacks="True" CssClass="data-table" GridLines="None">
					<Columns>
						<asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True" SortExpression="id" />
						<asp:BoundField DataField="parent_id" HeaderText="parent_id" SortExpression="parent_id" />
						<asp:BoundField DataField="url" HeaderText="url" SortExpression="url" />
						<asp:BoundField DataField="sort" HeaderText="sort" SortExpression="sort" />
						<asp:BoundField DataField="name" HeaderText="name" SortExpression="name" />
						<asp:BoundField DataField="new_window" HeaderText="new_window" SortExpression="new_window" />
						<asp:CommandField ShowEditButton="True" ShowDeleteButton="True" EditText="edit" CancelText="cancel" DeleteText="del" UpdateText="upd" />
					</Columns>
					<AlternatingRowStyle CssClass="odd" />
				</asp:GridView>
				<asp:SqlDataSource ID="SqlDataSourceMenu" runat="server" ConnectionString="<% =Global.ConnectionString() %>"
						ProviderName="<%$ ConnectionStrings:ConnectionString.providerName%>"
						SelectCommandType="StoredProcedure" SelectCommand="MenuItemsGetAll"
						DeleteCommandType="StoredProcedure" DeleteCommand="MenuItemsDel"
						UpdateCommandType="StoredProcedure" UpdateCommand="MenuItemsUpdate">
					<DeleteParameters>
						<asp:Parameter Name="id" Type="Int32"  Direction="Input" DefaultValue="-1"/>
					</DeleteParameters>
					<UpdateParameters>
						<asp:Parameter Name="id"         Type="Int32"  Direction="Input" DefaultValue="-1" />
						<asp:Parameter Name="parent_id"  Type="Int32"  Direction="Input" DefaultValue="0"/>
						<asp:Parameter Name="name"       Type="String" Direction="Input" DefaultValue="0" />
						<asp:Parameter Name="url"        Type="String" Direction="Input" DefaultValue="-1" />
						<asp:Parameter Name="new_window" Type="Int32"  Direction="Input" DefaultValue="1"/>
						<asp:Parameter Name="sort"       Type="Int32"  Direction="Input" DefaultValue="0" />
					</UpdateParameters>
				</asp:SqlDataSource>
			</li>
		</ul>
	</div>
</asp:Content>
