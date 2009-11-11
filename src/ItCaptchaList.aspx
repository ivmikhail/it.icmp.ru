<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItCaptchaList.aspx.cs" Inherits="ITCommunity.ItCaptchaList" Title="ItCaptchaList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<asp:GridView ID="gridList" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" GridLines="None"
		AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="30" CssClass="data-table" Caption="Список IT-каптчей">
		<Columns>
			<asp:BoundField DataField="text" HeaderText="Вопрос" />
			<asp:HyperLinkField Text="Изменить" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/ItCaptchaEdit.aspx?id={0}"/>
			<asp:HyperLinkField Text="Удалить" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/ItCaptchaEdit.aspx?del={0}"/>
		</Columns>
		<AlternatingRowStyle CssClass="odd" />
	</asp:GridView>

	<div class="big-button">
		<asp:HyperLink ID="hplAdd" runat="server" Text="Добавить" NavigateUrl="~/ItCaptchaEdit.aspx?new=1" EnableViewState="false" />
	</div>
</asp:Content>
