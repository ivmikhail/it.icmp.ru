<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" Inherits="ITCommunity.Accounts" Title="Ykt IT Community | Управление пользователями" %>

<%@ Register Src="~/controls/UsersList.ascx" TagName="UsersList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<script type="text/javascript">
		window.addEvent('domready', init);
		function init() {
			myTabs = new TabSwapper({
						selectedClass: 'on',
						deselectedClass: 'off',
						tabs: $$('#accounts-tabs li'),
						clickers: $$('#accounts-tabs li a'),
						sections: $$('div.sections div.panel'),
						cookieName: 'accounts-tabs',
						smooth: true,
						smoothSize: true
					 });
			}
	</script>

	<h1>Управление пользователями</h1>
	<div class="note">
		<h3>Существует 4 роли пользователей:</h3>
		<ul>
			<li>
				<b class="user-role">user</b> - простой пользователь, может голосовать, комментировать без капчи, доступные закрытые разделы сайтов. Readonly короче
			</li>        
			<li>
				<b class="user-role">poster</b> - права роли <b class="user-role">user</b> + может добавлять/редактировать свои новости(является ролью по умолчанию)
			</li>
			<li>
				<b class="user-role">admin</b> - права роли <b class="user-role">poster</b> + может управлять опросами, пользователями, может аттачить посты, редактировать любые посты. Так же управляет менюшкой, капчей, категориями новостей
			</li>
			<li>
				<b class="user-role">banned</b> - аккаунт забанен(залогиниться нельзя)
			</li>
		</ul>
	</div>

	<h2>Присвоить определенную роль пользователю</h2>
	<div class="note">
		Администратор помни! При регистрации пользователю присваивается роль <b class="user-role">poster</b>
	</div>

	<label class="textbox-input">
		<h3>Логин пользователя</h3>
		<asp:TextBox ID="UserLogin" runat="server" />
	</label>

	<label class="dropdown-list-select">
		<h3>Присвоить роль</h3>  
		<asp:DropDownList ID="DropDownListActions" runat="server" />
	</label>

	<asp:Literal ID="ModifyUserMessage" runat="server" />

	<div class="big-button">
		<asp:LinkButton ID="ModifyUser" runat="server" OnClick="ModifyUser_Click">Сделать</asp:LinkButton>
	</div>

	<h2>Список пользователей по ролям</h2>
	<div id="accounts-tabs" class="tabs">
		<ul class="tabSet">
			<li class="off"><a>admin</a></li>
			<li class="off"><a>user</a></li>
			<li class="off"><a>banned</a></li>
		</ul>

		<div class="sections">
			<div class="panel">
				<uc:UsersList id="admins" runat="server" />
			</div>
			<div class="panel">
				<uc:UsersList id="users" runat="server" />
			</div>
			<div class="panel">
				<uc:UsersList id="banned" runat="server" />
			</div>
		</div> 
	</div>
</asp:Content>
