<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mail.aspx.cs" Inherits="ITCommunity.Mail" Title="Ykt IT Community | �������� ���������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>�������� ���������</h1>
    <ul class="list">
        <li>
            <h3>�� ����</h3>
            <asp:Literal ID="Sender" runat="server" />
        </li>
        <li>
            <h3>���������</h3>
            <asp:Literal ID="MessageTitle" runat="server" />
        </li>
        <li>
            <h3>�����</h3> 
            <asp:Literal ID="MessageText" runat="server" />                
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="DeleteLink" runat="server" OnClick="DeleteLink_Click">�������</asp:LinkButton>
            <asp:Literal ID="ReplyLink" runat="server" />
            <a href="mailview.aspx">��������� � ������</a>
        </li>
    </ul>
</asp:Content>

