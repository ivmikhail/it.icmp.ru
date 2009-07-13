<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" Inherits="ITCommunity.Accounts" Title="Ykt IT Community | ”правление пользовател€ми" %>
<%@ Register Src="~/controls/UserList.ascx" TagName="UserList" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">  
    <script type="text/javascript">
	    window.addEvent('domready', init);
	    function init() {
		    myTabs = new TabSwapper({
                        selectedClass: 'on',
                        deselectedClass: 'off',
                        tabs: $$('#accounts-tabs li'),
                        clickers: $$('#accounts-tabs li a'),
                        sections: $$('div.panelSet div.panel'),
                        cookieName: 'accounts-tabs',
                        smooth: true,
	                    smoothSize: true
                     });
	        }
    </script>
    <h1>”правление пользовател€ми</h1>
     <p>—уществует 4 роли пользователей:</p>   
     <ul class="list userroles-list">
        <li>
            <b>user</b> - простой пользователь, может голосовать, комментировать без капчи, доступные закрытые разделы сайтов. Readonly короче
        </li>        
        <li>
            <b>poster</b> - права роли "user" + может добавл€ть/редактировать свои новости(€вл€етс€ ролью по умолчанию)
        </li>        
        <li>
            <b>admin</b> - права роли "poster" + может управл€ть опросами, пользовател€ми, может аттачить посты, редактировать любые посты. “ак же управл€ет менюшкой, капчей, категори€ми новостей
        </li>        
        <li>
            <b>banned</b> - аккаунт забанен(залогинитьс€ нельз€)
        </li>
    </ul> 
    <h2>ѕрисвоить определенную роль пользователю</h2>       
        <p>јдминистратор помни! ѕри регистрации пользователю присваиваетс€ роль poster</p>
        <div id="modify-user">
            <ul class="list">
                <li>
                    <h3>Ћогин пользовател€</h3> 
                    <asp:TextBox ID="UserLogin" runat="server" Width="100%"/>           
                </li>
                <li>
                     <h3>ѕрисвоить роль</h3>  
                     <asp:DropDownList ID="DropDownListActions" runat="server" Width="101%"/>      
                </li>                
                <li style="text-align:right; padding-top:10px;">
                    <asp:LinkButton ID="ModifyUser" runat="server" OnClick="ModifyUser_Click">—делать</asp:LinkButton>
                </li>
                <li>
                    <div class="error-message">
                        <asp:Literal ID="ModifyUserMessage" runat="server" />
                    </div>
                </li>
            </ul>    
        </div>
    <h2>—писок пользователей по рол€м</h2>    
        <div id="accounts-tabs">
	        <ul class="tabSet">
		        <li class="off"><a>admin</a></li>
		        <li class="off"><a>user</a></li>
		        <li class="off"><a>banned</a></li>
	        </ul>
	    
            <div class="panelSet">
	            <div class="panel">
	                <uc:UserList id="admins" runat="server" />
	            </div>
	            <div class="panel">
	                <uc:UserList id="users" runat="server" />
	            </div>
	            <div class="panel">
	                <uc:UserList id="banned" runat="server" />
	            </div>
            </div> 
        </div>
</asp:Content>

