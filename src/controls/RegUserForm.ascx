<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegUserForm.ascx.cs" Inherits="ITCommunity.RegUserForm" %>
<%@ Register src="~/controls/ItCaptcha.ascx"   tagname="ItCaptcha" tagprefix="uc" %>

<div id="reguser_panel">
    <div id="reguser_data">
        <ul class="list">
            <li>
                <h2>логин(аккаунт)</h2>  
                <label> 
                    <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateRegData" CssClass="input-text" MaxLength="32"/>
                </label>
            </li>        
            <li>
                 <h2>электропочта</h2>   
                 <label>
                    <asp:TextBox ID="TextBoxEmail" runat="server" ValidationGroup="ValidateRegData" CssClass="input-text" MaxLength="512"/>
                 </label>
            </li>            
            <li>
                <h2>пароль</h2>
                <label>
                    <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateRegData" TextMode="Password" CssClass="input-text" MaxLength="512"/>
                </label>
            </li>            
            <li>
                <h2>повторите пароль</h2>
                <label>
                    <asp:TextBox ID="TextBoxPassConf" runat="server" ValidationGroup="ValidateRegData" TextMode="Password" CssClass="input-text" MaxLength="512"/>
                </label>
            </li>
            <li>            
                <uc:ItCaptcha ID="captcha" runat="server" Visible="true" EnableViewState="true"/>
            </li>
            <li class="register-button">            
                <asp:LinkButton ID="RegisterButton" runat="server" ValidationGroup="ValidateRegData" OnClick="RegisterButton_Click">зарегистрируйте меня, я готов!</asp:LinkButton>
            </li>
         </ul>
         
     </div>
     <div class="reguser_messages">
        <asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateRegData" DisplayMode="List"  />
        
        <asp:RequiredFieldValidator     ID="RequiredLogin" 
                                        runat="server" 
                                        ControlToValidate="TextBoxLogin"
                                        ErrorMessage="Введите логин." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData" />
        <asp:RegularExpressionValidator ID="LoginValidator" 
                                        runat="server" 
                                        ControlToValidate="TextBoxLogin" 
                                        ErrorMessage="Логин может состоять только из латинских символов, цифр, знаков '-' и '_'. Длина должна быть от 3-х до 25-и символов." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData"
                                        ValidationExpression="^[A-z0-9\-_!\. ]{3,25}$" /> 
        
        
        <asp:RequiredFieldValidator     ID="RequiredEmail" 
                                        runat="server" 
                                        ControlToValidate="TextBoxEmail"
                                        ErrorMessage="Введите e-mail." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData" />
        <asp:RegularExpressionValidator ID="EmailValidator" 
                                        runat="server" 
                                        ControlToValidate="TextBoxEmail" 
                                        ErrorMessage="Введите нормальный e-mail." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"/>
            
        
        <asp:RequiredFieldValidator ID="RequiredPass" 
                                    runat="server" 
                                    ControlToValidate="TextBoxPass"
                                    ErrorMessage="Введите пароль." 
                                    Display="None" 
                                    ValidationGroup="ValidateRegData" />
                                    
        <asp:CompareValidator       ID="ConfirmPassword" 
                                    runat="server" 
                                    Display="None"
                                    ControlToCompare="TextBoxPass" 
                                    ControlToValidate="TextBoxPassConf" 
                                    ErrorMessage="Пароли не совпадают." 
                                    ValidationGroup="ValidateRegData" />
                                    
         
        <asp:CustomValidator ID="AccountExist" 
                             runat="server" 
                             Display="None" 
                             ErrorMessage="Пользователь с таким логином/email'ом уже зарегистрирован." 
                             ValidationGroup="ValidateRegData" />
                             
       <asp:CustomValidator ID="AnonymousAccount" 
                             runat="server" 
                             Display="None" 
                             ErrorMessage="Нельзя регистрировать аккаунт с логином 'anonymous', выберите другой логин" 
                             ValidationGroup="ValidateRegData" />
                             
        <asp:CustomValidator ID="RegisterFailed" 
                             runat="server" 
                             Display="None"
                             ErrorMessage="Ошибка, регистрация не прошла, обратитесь к администратору." 
                             ValidationGroup="ValidateRegData" />
     </div>     
</div>