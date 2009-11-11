<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notfound.aspx.cs" Inherits="ITCommunity.NotFoundPage" Title="Ykt IT Community | Страница не найдена" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1 class="error">Запрашиваемая страница не найдена</h1>

	<div class="note">
		<div>
			Страница или файл, которую вы искали, уже переехала на новый адрес, либо перемещена/удалена. Воспользуйтесь <a href="search.aspx" title="Найти пропажу поискав по сайту">поиском</a> чтобы найти то, что вы искали.
		</div>
		<asp:Literal ID="LiteralReferrerUrl" runat="server"/>
		<div>
			Если вы считаете что нужно известить администраторов, то можете написать письмо по адресу <a href="mailto:it.icmp.ru@gmail.com">it.icmp.ru@gmail.com</a>
		</div>

		<h1>Вот несколько популярных причин появления этой страницы</h1>
		<ul class="list">
			<li><b>Устаревшая закладка</b> в вашем браузере</li>
			<li>Сервис поиска, который дал вам эту ссылку, <b>ещё не запомнил новое строение сайта</b></li>
			<li>Вы <b>опечатались при введении адреса</b></li>
		</ul>
	</div>
</asp:Content>
