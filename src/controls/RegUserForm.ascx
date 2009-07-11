<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegUserForm.ascx.cs" Inherits="ITCommunity.RegUserForm" %>
<%@ Register src="~/controls/ItCaptcha.ascx"   tagname="ItCaptcha" tagprefix="uc" %>

<div id="reguser_panel">
    <div id="reguser_data">
        <ul class="list">
            <li>
                <h2>�����(�������)</h2>  
                <label> 
                    <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateRegData" CssClass="input-text" MaxLength="32"/>
                </label>
            </li>        
            <li>
                 <h2>������������</h2>   
                 <label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" ValidationGroup="ValidateRegData" CssClass="input-text" MaxLength="512"/>
                 </label>
            </li>            
            <li>
                <h2>������</h2>
                <label>
                    <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateRegData" TextMode="Password" CssClass="input-text" MaxLength="512"/>
                </label>
            </li>            
            <li>
                <h2>��������� ������</h2>
                <label>
                    <asp:TextBox ID="TextBoxPassConf" runat="server" ValidationGroup="ValidateRegData" TextMode="Password" CssClass="input-text" MaxLength="512"/>
                </label>
            </li>
            <li>            
                <uc:ItCaptcha ID="captcha" runat="server" Visible="true" EnableViewState="true"/>
            </li>
            <li class="register-button">            
                <asp:LinkButton ID="RegisterButton" runat="server" ValidationGroup="ValidateRegData" OnClick="RegisterButton_Click">��������������� ����, � �����!</asp:LinkButton>
            </li>
         </ul>
         
     </div>
     <div class="reguser_messages">
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
                                        ErrorMessage="����� ����� �������� ������ �� ��������� ��������, ����, ������ '-' � '_'. ����� ������ ���� �� 3-� �� 25-� ��������." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData"
                                        ValidationExpression="^[A-z0-9\-_!\. ]{3,25}$" /> 
        
        
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
                             ErrorMessage="������������ � ����� �������/email'�� ��� ���������������." 
                             ValidationGroup="ValidateRegData" />
                             
       <asp:CustomValidator ID="AnonymousAccount" 
                             runat="server" 
                             Display="None" 
                             ErrorMessage="������ �������������� ������� � ������� 'anonymous', �������� ������ �����" 
                             ValidationGroup="ValidateRegData" />
                             
        <asp:CustomValidator ID="RegisterFailed" 
                             runat="server" 
                             Display="None"
                             ErrorMessage="������, ����������� �� ������, ���������� � ��������������." 
                             ValidationGroup="ValidateRegData" />
     </div>     
</div>