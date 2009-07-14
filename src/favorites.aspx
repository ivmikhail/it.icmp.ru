<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="favorites.aspx.cs" Inherits="ITCommunity.Favorites" Title="Ykt IT Community | Избранные новости" EnableViewState="false"%>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PostsView.ascx" TagName="PostsView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" src="media/js/float-pager.js" charset="utf-8"></script>
    
    <div id="posts-container">
        <uc:PostsView id="PostsView" runat="server" />
    </div>
    <div id="pager-container">
        <uc:Pager id="FavoritesPager" runat="server" />
    </div>    
</asp:Content>

