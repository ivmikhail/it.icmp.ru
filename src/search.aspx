<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="ITCommunity.Search" Title="Ykt IT Community | �����" %>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PostsView.ascx" TagName="PostsView" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>����� ��������</h2>
    <div>
        <asp:TextBox ID="TextBoxQuery" runat="server" />   
        <asp:LinkButton ID="LinkButtonSearch" runat="server" OnClick="LinkButtonSearch_Click">�����</asp:LinkButton>
    </div>
    <uc:PostsView id="FindedPosts" runat="server" />
    <uc:Pager     id="FindedPostsPager" runat="server" />
    
    <div id="search-notfound">
        <asp:Literal ID="NotFoundText" runat="server" Visible="false" Text="������ �� �������..." />
    </div>
</asp:Content>

