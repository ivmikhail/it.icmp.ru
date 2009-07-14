<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notfound.aspx.cs" Inherits="ITCommunity.NotFoundPage" Title="Ykt IT Community | —траница не найдена" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1 class="error-message">«апрашиваема€ страница не найдена</h1>
    <p>
        —траница, которую вы искали, уже переехала на новый адрес, либо перемещена/удалена. ¬оспользуйтесь <a href="search.aspx" title="Ќайти пропажу поискав по сайту">поиском</a> чтобы найти то, что вы искали.       
    </p>    
    <asp:Literal ID="LiteralReferrerUrl" runat="server"/>    
    <p>
        ≈сли вы считаете что нужно известить администраторов, то можете написать письмо по адресу <a href="mailto:ykt.itcommunity@gmail.com">ykt.itcommunity@gmail.com</a>
    </p>

    <h1>¬от несколько попул€рных причин по€влени€ этой страницы</h1>
        <ol style="list-style:a;">
	        <li><strong>”старевша€ закладка</strong> в вашем браузере</li>
	        <li>—ервис поиска, который дал вам эту ссылку, <strong>ещЄ не запомнил новое строение сайта</strong></li>
	        <li>¬ы <strong>опечатались при введении адреса</strong></li>
        </ol>

</asp:Content>

