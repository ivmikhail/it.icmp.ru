<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="favorites.aspx.cs" Inherits="ITCommunity.Favorites" Title="Ykt IT Community | Избранные новости" %>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PostsView.ascx" TagName="PostsView" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Избранные новости</h2>
    <uc:PostsView id="PostsView" runat="server" />
    <uc:Pager     id="FavoritesPager" runat="server" />
</asp:Content>

