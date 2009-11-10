<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="favorites.aspx.cs" Inherits="ITCommunity.Favorites" Title="Ykt IT Community | Избранные новости" EnableViewState="false"%>

<%@ Register Src="~/controls/PostsList.ascx" TagName="PostsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<uc:PostsList id="FavoritesList" runat="server" />

</asp:Content>
