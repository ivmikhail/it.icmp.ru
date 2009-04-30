<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rfc.aspx.cs" Inherits="ITCommunity.RfcPage" Title="Ykt IT Community" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="rfc-search">
    <h2>¬ведите номер RFC или ключевое слово</h2>
    <asp:TextBox ID="TextBoxSearch" runat="server" /> <asp:LinkButton ID="LinkButtonSearch" runat="server" OnClick="LinkButtonSearch_Click">»скать</asp:LinkButton>
    <asp:RequiredFieldValidator ID="SearchRequiredFieldValidator" runat="server" ErrorMessage="¬ведите чего-нибудь" ControlToValidate="TextBoxSearch" />
</div> 
    
 <asp:Repeater ID="RepeaterRfc" runat="server">
        <HeaderTemplate>
            <ul id="rfc-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="rfc"> 
                    <h2><%# Eval("number")%></h2>
                    <div>
                        <%# Eval("title")%>
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

