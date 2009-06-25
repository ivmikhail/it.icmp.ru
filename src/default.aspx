<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="ITCommunity.Default" Title="Ykt IT Community | Главная" Trace="true" %>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PostsView.ascx" TagName="PostsView" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="posts-container">
        <uc:PostsView id="PostsView" runat="server" />
    </div>
    <div id="pager-container">
        <uc:Pager     id="NewsPager" runat="server" />
    </div>
</asp:Content>

