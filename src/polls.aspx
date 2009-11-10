<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="polls.aspx.cs" Inherits="ITCommunity.PollsArchive" Title="Ykt IT Community | Результаты опросы" %>

<%@ Register Src="~/controls/PollsList.ascx" TagName="PollsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<uc:PollsList id="PollsList" runat="server" />

</asp:Content>
