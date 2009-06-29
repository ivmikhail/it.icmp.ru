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
    <h1>—брос парол€</h1>
    <p>¬ведите ниже ваш логин, ссылка дл€ сброса парол€ будет отправлена на ваш e-mail указанный при регистрации</p>
    <br />
    <ul class="list">
        <li>
            ¬ведите ваш логин <asp:TextBox ID="TextBoxLogin" runat="server" />
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
    <h1>—брос парол€</h1>
    <ul class="list">
        <li>
            <h2>¬аш логин</h2>
            <asp:Literal ID="RecoveryLogin" runat="server" />
        </li>
        <li>
            <h2>¬ведите ваш новый пароль</h2>
            <asp:TextBox ID="NewPass" runat="server" width="100%" TextMode="Password"/>
        </li>
        <li>
            <h2>¬ведите еще раз ваш новый пароль</h2>
            <asp:TextBox ID="NewPassConfirm" runat="server" width="100%" TextMode="Password"/>
        </li>        
        <li>
            <asp:LinkButton ID="LinkButtonChangePass" runat="server" OnClick="LinkButtonChangePass_Click" >изменить</asp:LinkButton>
        </li>
        <li>
                <asp:RequiredFieldValidator ID="RequiredPass" 
                                    runat="server" 
                                    ControlToValidate="NewPass"
                                    ErrorMessage="¬ведите пароль." 
                                    Display="None"  />
                <asp:CompareValidator       ID="ConfirmPassword" 
                                    runat="server" 
                                    Display="None"
                                    ControlToCompare="NewPass" 
                                    ControlToValidate="NewPassConfirm" 
                                    ErrorMessage="ѕароли не совпадают." />
        </li>
    </ul>
</div>

</asp:Content>

