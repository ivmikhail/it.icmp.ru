<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="ITCommunity.Register" Title="Ykt It Community | Регистрация" %>
<%@ Register src="~/controls/RegUserForm.ascx" tagname="RegUserForm" tagprefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc:RegUserForm ID="RegUser" runat="server" />
    <br />
    <asp:Literal ID="AboutRegister" runat="server" Text="Label"/>
</asp:Content>

