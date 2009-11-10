<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="ITCommunity.Search" Title="Ykt IT Community | Поиск" %>

<%@ Register Src="~/controls/PostsList.ascx" TagName="PostsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div id="search">
		<h1>Поиск новостей</h1>

		<div class="textbox-input">
			<asp:TextBox ID="TextBoxQuery" runat="server" />
			<span class="big-button">
				<asp:LinkButton ID="LinkButtonSearch" runat="server" OnClick="LinkButtonSearch_Click">Найти</asp:LinkButton>
			</span>
		</div>

		<uc:PostsList id="FindedPosts" runat="server" />

		<asp:Literal ID="NotFoundText" runat="server" Visible="false">
			<h1 class="error">
				Ничего не найдено
			</h1>
		</asp:Literal>
	</div>
</asp:Content>
