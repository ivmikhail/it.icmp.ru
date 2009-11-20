<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserMenu.ascx.cs" Inherits="ITCommunity.UserMenu" %>

<div id="user-menu" class="menu-panel">
	<h1><asp:Literal ID="UserGreetingText" runat="server" /></h1>
	<ul>
		<li class="menu-text">
			UserRole - <asp:Literal ID="UserRoleText" runat="server" />
		</li>
		<li>
			<a href="editpost.aspx" title="Добавить свою новость">Добавить пост</a>
		</li>
		<li>
			<a href="mailview.aspx" title="Мои сообщения">Сообщения (<asp:Literal ID="NewMessagesCountText" runat="server" />)</a>
		</li>
		<li>
			<a href="notepad.aspx" title="Посмотреть записи">Блокнот</a>
		</li>
		<li>
			<a href="favorites.aspx" title="Статьи которые я отметил">Избранное</a>
		</li>
		<li>
			<a href="profile.aspx" title="Изменить email или пароль">Профиль</a>
		</li>
		<li>
			<a href="addheadertext.aspx" title="Добавить текст для хидера" class="new-link">Текст для хидера</a>
		</li>
		<li>
			<asp:LinkButton ID="LinkButtonExit" runat="server" OnClick="LinkButtonExit_Click" ToolTip="Выйти из аккаунта">Выйти</asp:LinkButton>
		</li>
	</ul>
</div>
