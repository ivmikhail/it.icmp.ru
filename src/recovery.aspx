<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="recovery.aspx.cs" Inherits="ITCommunity.Recovery" Title="Ykt IT Community" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div id="SendRecoveryLink" runat="server" visible="false">
    <h1>����� ������</h1>
    <p>������� ���� ��� �����, ������ ��� ������ ������ ����� ���������� �� ��� e-mail ��������� ��� �����������</p>
    <ul class="list">
        <li>
            ������� ��� ����� <asp:TextBox ID="TextBoxLogin" runat="server" />
            <asp:LinkButton ID="LinkButtonSendEmail" runat="server" OnClick="LinkButtonSendEmail_Click">������������</asp:LinkButton>
        </li>
        <li>
            <div class="error-message">
                <asp:Literal ID="SendRecoveryErrors" runat="server" />
            </div>
        </li>
    </ul>
</div>
<div id="RecoveryPassContainer" runat="server" visible="false">
    <h1>����� ������</h1>
    <ul class="list">
        <li>
            <h2>��� �����</h2>
            <asp:Literal ID="RecoveryLogin" runat="server" />
        </li>
        <li>
            <h2>������� ��� ����� ������</h2>
            <asp:TextBox ID="NewPass" runat="server" width="100%" TextMode="Password"/>
        </li>
        <li>
            <h2>������� ��� ��� ��� ����� ������</h2>
            <asp:TextBox ID="NewPassConfirm" runat="server" width="100%" TextMode="Password"/>
        </li>        
        <li>
            <asp:LinkButton ID="LinkButtonChangePass" runat="server" OnClick="LinkButtonChangePass_Click">��������</asp:LinkButton>
        </li>
        <li>
                <asp:RequiredFieldValidator ID="RequiredPass" 
                                    runat="server" 
                                    ControlToValidate="NewPass"
                                    ErrorMessage="������� ������." 
                                    Display="None"  />
                <asp:CompareValidator       ID="ConfirmPassword" 
                                    runat="server" 
                                    Display="None"
                                    ControlToCompare="NewPass" 
                                    ControlToValidate="NewPassConfirm" 
                                    ErrorMessage="������ �� ���������." />
        </li>
    </ul>
</div>

</asp:Content>

