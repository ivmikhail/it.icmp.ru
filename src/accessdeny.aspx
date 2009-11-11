<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accessdeny.aspx.cs" Inherits="ITCommunity.AccessDenyPage" Title="Ykt IT Community | доступ запрещен" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<h1 class="error">Доступ к данной странице для вас запрещен</h1>

	<div class="note">
		Для получения доступа попробуйте авторизоваться слева или <a href='register.aspx' title='Зарегистрировать нового пользователя'>зарегистрироваться</a>.
		<br />
		Возможно вы и не должны иметь доступ к этой странице...
	</div>
</asp:Content>
