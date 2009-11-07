<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="recovery.aspx.cs" Inherits="ITCommunity.Recovery" Title="Ykt IT Community" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    function wait4mail()
    {
        var emailWaiter = new Waiter($('<%= SendRecoveryLink.ClientID %>'), {
            baseHref: '..',
            img: {
                src: 'media/img/design/waiter.gif',
                id: 'waitingImg',
                styles: {
                    position: 'absolute',
                    width: 24,
                    height: 24,
                    display: 'none',
                    opacity: 0,
                    zIndex: 999
                }
            },
            layer: {
                id: 'waitingDiv',
                background: '#c1d2ee',
                opacity: 0.9
             }
        });
        emailWaiter.start();
        emailWaiter.stop.delay(20000, emailWaiter); 
    }
</script>
<div id="SendRecoveryLink" runat="server" visible="false">
    <h1>Сброс пароля</h1>
    <p>Введите ниже ваш логин, ссылка для сброса пароля будет отправлена на ваш e-mail указанный при регистрации</p>
    <br />
    <ul class="list">
        <li>
            Введите ваш логин <asp:TextBox ID="TextBoxLogin" runat="server" />
            <asp:LinkButton ID="LinkButtonSendEmail" runat="server" OnClick="LinkButtonSendEmail_Click" OnClientClick="wait4mail();">восстановить</asp:LinkButton>
        </li>
        <li>
            <div class="error-message">
                <asp:Literal ID="SendRecoveryErrors" runat="server" />
            </div>
        </li>
    </ul>
</div>
<div id="RecoveryPassContainer" runat="server" visible="false">
    <h1>Сброс пароля</h1>
    <ul class="list">
        <li>
            <h2>Ваш логин</h2>
            <asp:Literal ID="RecoveryLogin" runat="server" />
        </li>
        <li>
            <h2>Введите ваш новый пароль</h2>
            <asp:TextBox ID="NewPass" runat="server" width="100%" TextMode="Password"/>
        </li>
        <li>
            <h2>Введите еще раз ваш новый пароль</h2>
            <asp:TextBox ID="NewPassConfirm" runat="server" width="100%" TextMode="Password"/>
        </li>        
        <li>
            <asp:LinkButton ID="LinkButtonChangePass" runat="server" OnClick="LinkButtonChangePass_Click" >изменить</asp:LinkButton>
        </li>
        <li>
                <asp:RequiredFieldValidator ID="RequiredPass" 
                                    runat="server" 
                                    ControlToValidate="NewPass"
                                    ErrorMessage="Введите пароль." 
                                    Display="None"  />
                <asp:CompareValidator       ID="ConfirmPassword" 
                                    runat="server" 
                                    Display="None"
                                    ControlToCompare="NewPass" 
                                    ControlToValidate="NewPassConfirm" 
                                    ErrorMessage="Пароли не совпадают." />
        </li>
    </ul>
</div>

</asp:Content>

