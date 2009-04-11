<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="LoginForm" %>
<div id="auth_panel">
    <div>
        <label>
            логин   
            <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateAuthData" />
         </label>
         <label>
            пароль
            <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateAuthData" TextMode="Password"/>
        </label>
        <label>
            <asp:CheckBox ID="CheckBoxIsRemember" runat="server" />        
            запомнить            
        </label>
     </div>
     <div id="auth_messages">
        <asp:ValidationSummary ID="ValidationSummaryAuth" runat="server" ValidationGroup="ValidateAuthData" DisplayMode="List"  />
        
        <asp:RequiredFieldValidator ID="RequiredLogin" runat="server" ControlToValidate="TextBoxLogin"
            ErrorMessage="Введите логин." Display="None" ValidationGroup="ValidateAuthData" />
        <asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="TextBoxPass"
            ErrorMessage="Введите пароль." Display="None" ValidationGroup="ValidateAuthData" />
        <asp:CustomValidator ID="WrongAccount" runat="server" Display="None" 
            ErrorMessage="Неправильный логин/пароль." ValidationGroup="ValidateAuthData" />
        <asp:CustomValidator ID="UserIsBanned" runat="server" Display="None" 
            ErrorMessage="Вы забанены." ValidationGroup="ValidateAuthData" />
     </div>     
    <asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click" ValidationGroup="ValidateAuthData">Вход</asp:LinkButton>
</div>