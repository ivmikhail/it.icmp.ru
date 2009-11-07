<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notfound.aspx.cs" Inherits="ITCommunity.NotFoundPage" Title="Ykt IT Community | Страница не найдена" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="error-message">Запрашиваемая страница не найдена</h1>
    <p>
        Страница или файл, которую вы искали, уже переехала на новый адрес, либо перемещена/удалена. Воспользуйтесь <a href="search.aspx" title="Найти пропажу поискав по сайту">поиском</a> чтобы найти то, что вы искали.       
    </p>    
    <asp:Literal ID="LiteralReferrerUrl" runat="server"/>    
    <p>
        Если вы считаете что нужно известить администраторов, то можете написать письмо по адресу <a href="mailto:it.icmp.ru@gmail.com">it.icmp.ru@gmail.com</a>
    </p>

    <h1>Вот несколько популярных причин появления этой страницы</h1>
        <ol style="list-style:a;">
	        <li><strong>Устаревшая закладка</strong> в вашем браузере</li>
	        <li>Сервис поиска, который дал вам эту ссылку, <strong>ещё не запомнил новое строение сайта</strong></li>
	        <li>Вы <strong>опечатались при введении адреса</strong></li>
        </ol>

</asp:Content>

