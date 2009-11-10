<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accessdeny.aspx.cs" Inherits="ITCommunity.AccessDenyPage" Title="Ykt IT Community | доступ запрещен" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="error">
	<h1>Доступ к данной странице для вас запрещен</h1>
	<asp:Literal ID="LiteralMessage" runat="server" />
</div>
</asp:Content>

