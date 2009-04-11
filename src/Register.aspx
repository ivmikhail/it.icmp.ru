<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" Title="Untitled Page" %>
<%@ Register src="~/Controls/RegUserForm.ascx" tagname="RegUserForm" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:RegUserForm ID="RegUser" runat="server" />
</asp:Content>

