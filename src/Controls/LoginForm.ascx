<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="ITCommunity.LoginForm" %>
<div id="auth_panel">
<h2>�����������</h2>
    <div id="auth_data">
        <ul class="list">
            <li>   
                �����        
                <asp:TextBox ID="TextBoxLogin" runat="server"  CssClass="short-input" ValidationGroup="ValidateAuthData" />
            </li>            
            <li>     
                ������       
                <asp:TextBox ID="TextBoxPass" runat="server" CssClass="short-input" ValidationGroup="ValidateAuthData" TextMode="Password"/>
            </li>
            
            <li>   
                ��������� <asp:CheckBox ID="CheckBoxIsRemember" runat="server" />    
            </li>
            <li>            
                <asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click" ValidationGroup="ValidateAuthData">����</asp:LinkButton>
                <a href="register.aspx" title="�������������� � ����� ��������� ����������">�����������</a>              
            </li>
            <li>
                <a href="recovery.aspx" title="��� ���� ���� ����� ������">� ����� ������</a>
            </li>
        </ul>        
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
</div>