<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="testpage.aspx.cs" Inherits="ITCommunity.TestPage" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>ТЕСТОВАЯ СТРАНИЦА ЕПТЫТЬ!(можно выбросить, но пусть будет)</h1>
<br />

<asp:TextBox ID="Input" runat="server" TextMode="MultiLine" Rows="20" Width="50%">
</asp:TextBox>

<br />
<br />

<asp:LinkButton ID="Button" runat="server" OnClick="Button_Click">LinkButton</asp:LinkButton>

<br />
<br />

Результат
<br />
<hr />

<asp:Literal ID="Output" runat="server"></asp:Literal>

<hr />

</asp:Content>

