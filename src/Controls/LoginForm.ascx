<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="LoginForm" %>
<div id="auth_panel">
    <div>
        <label>
            �����   
            <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateAuthData" />
         </label>
         <label>
            ������
            <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateAuthData" TextMode="Password"/>
        </label>
        <label>
            <asp:CheckBox ID="CheckBoxIsRemember" runat="server" />        
            ���������            
        </label>
     </div>
     <div id="auth_messages">
        <asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateAuthData" DisplayMode="List"  />
        
        <asp:RequiredFieldValidator ID="RequiredLogin" runat="server" ControlToValidate="TextBoxLogin"
            ErrorMessage="������� �����." Display="None" ValidationGroup="ValidateAuthData" />
        <asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="TextBoxPass"
            ErrorMessage="������� ������." Display="None" ValidationGroup="ValidateAuthData" />
        <asp:CustomValidator ID="WrongAccount" runat="server" Display="None" 
            ErrorMessage="������������ �����/������." ValidationGroup="ValidateAuthData" />
        <asp:CustomValidator ID="UserIsBanned" runat="server" Display="None" 
            ErrorMessage="�� ��������." ValidationGroup="ValidateAuthData" />
     </div>     
    <asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click" ValidationGroup="ValidateAuthData">����</asp:LinkButton>
</div>