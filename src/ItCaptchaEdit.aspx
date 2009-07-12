<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItCaptchaEdit.aspx.cs" Inherits="ITCommunity.ItCaptchaEdit" Title="ItCaptchaEdit" %>
<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtQuestion" runat="server" width="90%" />
    <asp:PlaceHolder ID="pnlAnsewrs" runat="server">
    
    </asp:PlaceHolder><br />
    <asp:Button ID="btnAdd" runat="server" Text="add" OnClick="btnAdd_Click" />
</asp:Content>

