<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegUserForm.ascx.cs" Inherits="RegUserForm" %>
<div id="reguser_panel">
    <div>
        <label>
            логин   
            <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateRegData" />
         </label>
         <label>
            e-mail   
            <asp:TextBox ID="TextBoxEmail" runat="server" ValidationGroup="ValidateRegData" />
         </label>
         <label>
            пароль
            <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateRegData" TextMode="Password"/>
         </label>
         <label>
            повторите пароль
            <asp:TextBox ID="TextBoxPassConf" runat="server" ValidationGroup="ValidateRegData" TextMode="Password"/>
         </label>
     </div>
     <div id="reguser_messages">
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
                                        ErrorMessage="Логин может состоять только из латинских символов, цифр, знаков '-' и '-'. Длина должна быть от 2-х до 32-х символов." 
                                        Display="None" 
                                        ValidationGroup="ValidateRegData"
                                        ValidationExpression="^[A-z0-9\-_!\. ]{2,32}$" /> 
        
        
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
                             ErrorMessage="Пользователь с таким логином уже зарегистрирован." 
                             ValidationGroup="ValidateRegData" />
                             
        <asp:CustomValidator ID="RegisterFailed" 
                             runat="server" 
                             Display="None"
                             ErrorMessage="Ошибка, регистрация не прошла, обратитесь к администратору." 
                             ValidationGroup="ValidateRegData" />
     </div>     
    <asp:LinkButton ID="RegisterButton" runat="server" ValidationGroup="ValidateRegData" OnClick="RegisterButton_Click">Зарегаться</asp:LinkButton>
</div>