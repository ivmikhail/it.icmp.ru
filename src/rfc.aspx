<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="rfc.aspx.cs" Inherits="ITCommunity.RfcPage" Title="Ykt IT Community" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div id="rfc-search">
    <h2>������� ����� RFC ��� �������� �����</h2>
    <asp:TextBox ID="TextBoxSearch" runat="server" /> 
    <asp:LinkButton ID="LinkButtonSearch" runat="server" OnClick="LinkButtonSearch_Click" ValidationGroup="ValidateRfcSearch">������</asp:LinkButton>
    <asp:RequiredFieldValidator ID="SearchRequiredFieldValidator" runat="server" ErrorMessage="������� ����-������" ControlToValidate="TextBoxSearch" ValidationGroup="ValidateRfcSearch"/>
</div>     
<div id="rfc-search-result">
    <asp:Repeater ID="RepeaterRfc" runat="server">
        <HeaderTemplate>
            <ul id="rfc-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="rfc"> 
                     <h2> 
                        <a href='<%# FormURL(Eval("number").ToString())%>' title="���������� ������ �����"><%# Eval("number")%></a>
                    </h2>
                    <div class="rfc-title">
                        <%# Eval("title")%>
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    <div id="search-notfound">
        <asp:Literal ID="NotFoundText" runat="server" Visible="false" Text="������ �� �������..." />
    </div>
</div>
</asp:Content>

