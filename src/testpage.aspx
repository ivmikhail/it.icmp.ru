<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="testpage.aspx.cs" Inherits="ITCommunity.TestPage" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<h1>ТЕСТОВАЯ СТРАНИЦА ЕПТЫТЬ!(можно выбросить, но пусть будет)</h1>

	<div class="textbox-textarea">
		<asp:TextBox ID="Input" runat="server" TextMode="MultiLine" Rows="10" />
	</div>

	<div class="big-button">
		<asp:LinkButton ID="Button" runat="server" OnClick="Button_Click">LinkButton</asp:LinkButton>
	</div>
	
	<h2>Результат</h2>

	<asp:Literal ID="Output" runat="server" />

</asp:Content>
