<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="ITCommunity.Default" Title="Ykt IT Community | �������" %>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PostsView.ascx" TagName="PostsView" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>�������</h2>
    <uc:PostsView id="PostsView" runat="server" />
    <uc:Pager     id="NewsPager" runat="server" />
</asp:Content>

