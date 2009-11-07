<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="polls.aspx.cs" Inherits="ITCommunity.PollsArchive" Title="Ykt IT Community | Результаты опросы" %>

<%@ Register Src="~/controls/Pager.ascx"     TagName="Pager"     TagPrefix="uc" %>
<%@ Register Src="~/controls/PollsView.ascx" TagName="PollsView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Архив опросов</h1>
    <div id="polls-container">
        <uc:PollsView id="PollsView" runat="server" />
    </div>
    <div id="polls-pager-container">
        <uc:Pager id="PollsPager" runat="server" />
    </div>
</asp:Content>

