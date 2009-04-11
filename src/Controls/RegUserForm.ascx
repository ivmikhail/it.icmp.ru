<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegUserForm.ascx.cs" Inherits="RegUserForm" %>
<div id="reguser_panel">
    <div>
        <label>
            �����   
            <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateRegData" />
         </label>
         <label>
            e-mail   
            <asp:TextBox ID="TextBoxEmail" runat="server" ValidationGroup="ValidateRegData" />
         </label>
         <label>
            ������
            <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateRegData" TextMode="Password"/>
         </label>
         <label>
            ��������� ������
            <asp:TextBox ID="TextBoxPassConf" runat="server" ValidationGroup="ValidateRegData" TextMode="Password"/>
         </label>
     </div>
     <div id="reguser_messages">
        <asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateRegData" DisplayMode="List"  />
        
        <asp:RequiredFieldValidator     ID="RequiredLogin" 
                                        runat="server" 
                                        ControlToValidate="TextBoxLogin"
                                        ErrorMessage="������� �����." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData" />
        <asp:RegularExpressionValidator ID="LoginValidator" 
                                        runat="server" 
                                        ControlToValidate="TextBoxLogin" 
                                        ErrorMessage="����� ����� �������� ������ �� ��������� ��������, ����, ������ '-' � '-'. ����� ������ ���� �� 2-� �� 32-� ��������." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData"
                                        ValidationExpression="^[A-z0-9\-_!\. ]{2,32}$" /> 
        
        
        <asp:RequiredFieldValidator     ID="RequiredEmail" 
                                        runat="server" 
                                        ControlToValidate="TextBoxEmail"
                                        ErrorMessage="������� e-mail." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData" />
        <asp:RegularExpressionValidator ID="EmailValidator" 
                                        runat="server" 
                                        ControlToValidate="TextBoxEmail" 
                                        ErrorMessage="������� ���������� e-mail." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"/>
            
        
        <asp:RequiredFieldValidator ID="RequiredPass" 
                                    runat="server" 
                                    ControlToValidate="TextBoxPass"
                                    ErrorMessage="������� ������." 
                                    Display="None" 
                                    ValidationGroup="ValidateRegData" />
        <asp:CompareValidator       ID="ConfirmPassword" 
                                    runat="server" 
                                    Display="None"
                                    ControlToCompare="TextBoxPass" 
                                    ControlToValidate="TextBoxPassConf" 
                                    ErrorMessage="������ �� ���������." 
                                    ValidationGroup="ValidateRegData" />
                                    
         
        <asp:CustomValidator ID="AccountExist" 
                             runat="server" 
                             Display="None" 
                             ErrorMessage="������������ � ����� ������� ��� ���������������." 
                             ValidationGroup="ValidateRegData" />
                             
        <asp:CustomValidator ID="RegisterFailed" 
                             runat="server" 
                             Display="None"
                             ErrorMessage="������, ����������� �� ������, ���������� � ��������������." 
                             ValidationGroup="ValidateRegData" />
     </div>     
    <asp:LinkButton ID="RegisterButton" runat="server" ValidationGroup="ValidateRegData" OnClick="RegisterButton_Click">����������</asp:LinkButton>
</div>