<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accessdeny.aspx.cs" Inherits="ITCommunity.AccessDenyPage" Title="Ykt IT Community | доступ запрещен" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1 class="error-message">Доступ к данной странице для вас запрещен</h1>
    <asp:Literal ID="LiteralMessage" runat="server" />
</asp:Content>

