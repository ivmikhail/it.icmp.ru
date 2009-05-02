<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="ITCommunity.LoginForm" %>
<div id="auth_panel">
<h2>Авторизация</h2>
    <div id="auth_data">
        <ul class="list">
            <li>   
                логин        
                <label>
                    <asp:TextBox ID="TextBoxLogin" runat="server"  Width="100%" ValidationGroup="ValidateAuthData" />
                </label>
            </li>            
            <li>     
                пароль       
                <label>
                    <asp:TextBox ID="TextBoxPass" runat="server" Width="100%" ValidationGroup="ValidateAuthData" TextMode="Password"/>
                </label>
            </li>
            
            <li>   
                запомнить         
                <label>
                    <asp:CheckBox ID="CheckBoxIsRemember" runat="server" />     
                </label>
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