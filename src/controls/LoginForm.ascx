<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="ITCommunity.LoginForm" %>
<div id="auth_panel">
<h2>Авторизация</h2>
    <div id="auth_data">
        <ul class="list">
            <li>   
                логин        
                <asp:TextBox ID="TextBoxLogin" runat="server"  CssClass="short-input" ValidationGroup="ValidateAuthData" />
            </li>            
            <li>     
                пароль       
                <asp:TextBox ID="TextBoxPass" runat="server" CssClass="short-input" ValidationGroup="ValidateAuthData" TextMode="Password"/>
            </li>
            
            <li>   
                запомнить <asp:CheckBox ID="CheckBoxIsRemember" runat="server" />    
            </li>
            <li>            
                <asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click" ValidationGroup="ValidateAuthData">Вход</asp:LinkButton>
                <a href="register.aspx" title="Присоединиться к этому чудесному сообществу">Регистрация</a>              
            </li>
            <li>
                <a href="recovery.aspx" title="Жми сюда если забыл пароль">Я забыл пароль</a>
            </li>
        </ul>        
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
</div>