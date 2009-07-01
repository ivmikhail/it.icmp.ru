<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pollresult.aspx.cs" Inherits="ITCommunity.PollResultPage" Title="Ykt IT Community | Результаты опросы" %>
<%@ Register Src="~/controls/PollResults.ascx" TagName="PollResult" TagPrefix="uc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:PollResult ID="PollResult" runat="server" />
</asp:Content>

