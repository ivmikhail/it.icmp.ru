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
		<div class="note">
			Введите ниже ваш логин, ссылка для сброса пароля будет отправлена на ваш e-mail указанный при регистрации
		</div>
		<label class="textbox-input">
			<span class="label-title">Введите ваш логин</span>
			<asp:TextBox ID="TextBoxLogin" runat="server" />
		</label>
		
		<asp:Literal ID="SendRecoveryErrors" runat="server" />

		<div class="big-button">
			<asp:LinkButton ID="LinkButtonSendEmail" runat="server" OnClick="LinkButtonSendEmail_Click" OnClientClick="wait4mail();">восстановить</asp:LinkButton>
		</div>
	</div>

	<div id="RecoveryPassContainer" runat="server" visible="false">
		<h1>Сброс пароля</h1>
		<h2>Ваш логин - <asp:Literal ID="RecoveryLogin" runat="server" /></h2>
			
		<label class="textbox-input">
			<span class="label-title">Введите ваш новый пароль</span>
			<asp:TextBox ID="NewPass" runat="server" TextMode="Password"/>
		</label >

		<label class="textbox-input">
			<span class="label-title">Введите еще раз ваш новый пароль</span>
			<asp:TextBox ID="NewPassConfirm" runat="server" TextMode="Password"/>
		</label >

		<asp:RequiredFieldValidator ID="RequiredPass" runat="server" ControlToValidate="NewPass"
			ErrorMessage="Введите пароль." Display="None" />
		<asp:CompareValidator ID="ConfirmPassword" runat="server" Display="None"
			ControlToCompare="NewPass" ControlToValidate="NewPassConfirm" ErrorMessage="Пароли не совпадают." />

		<div class="big-button">
			<asp:LinkButton ID="LinkButtonChangePass" runat="server" OnClick="LinkButtonChangePass_Click" >изменить</asp:LinkButton>
		</div>
	</div>
</asp:Content>
