<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginForm.ascx.cs" Inherits="LoginForm" %>
<div id="auth_panel">
    <div id="auth_data">
        <ul>
            <li>            
                <label>
                    логин   
                    <asp:TextBox ID="TextBoxLogin" runat="server" ValidationGroup="ValidateAuthData" />
                </label>
            </li>            
            <li>            
                <label>
                    пароль
                    <asp:TextBox ID="TextBoxPass" runat="server" ValidationGroup="ValidateAuthData" TextMode="Password"/>
                </label>
            </li>
            
            <li>            
                <label>
                    запомнить 
                    <asp:CheckBox ID="CheckBoxIsRemember" runat="server" />     
                </label>
            </li>
            <li>            
                <asp:LinkButton ID="LogInButton" runat="server" OnClick="LogInButton_Click" ValidationGroup="ValidateAuthData">Вход</asp:LinkButton>
                <asp:HyperLink  ID="RegisterButton" runat="server" NavigateUrl="~/Register.aspx">Регистрация</asp:HyperLink>
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