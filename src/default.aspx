<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="ITCommunity.Default" Title="Ykt IT Community | Главная" %>

<%@ Register Src="~/controls/PostsList.ascx" TagName="PostsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<uc:PostsList id="PostsList" runat="server" />

</asp:Content>
