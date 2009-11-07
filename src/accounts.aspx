<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" Inherits="ITCommunity.Accounts" Title="Ykt IT Community | Управление пользователями" %>
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
    <h1>Управление пользователями</h1>
     <p>Существует 4 роли пользователей:</p>   
     <ul class="list userroles-list">
        <li>
            <b>user</b> - простой пользователь, может голосовать, комментировать без капчи, доступные закрытые разделы сайтов. Readonly короче
        </li>        
        <li>
            <b>poster</b> - права роли "user" + может добавлять/редактировать свои новости(является ролью по умолчанию)
        </li>        
        <li>
            <b>admin</b> - права роли "poster" + может управлять опросами, пользователями, может аттачить посты, редактировать любые посты. Так же управляет менюшкой, капчей, категориями новостей
        </li>        
        <li>
            <b>banned</b> - аккаунт забанен(залогиниться нельзя)
        </li>
    </ul> 
    <h2>Присвоить определенную роль пользователю</h2>       
        <p>Администратор помни! При регистрации пользователю присваивается роль poster</p>
        <div id="modify-user">
            <ul class="list">
                <li>
                    <h3>Логин пользователя</h3> 
                    <asp:TextBox ID="UserLogin" runat="server" Width="100%"/>           
                </li>
                <li>
                     <h3>Присвоить роль</h3>  
                     <asp:DropDownList ID="DropDownListActions" runat="server" Width="101%"/>      
                </li>                
                <li style="text-align:right; padding-top:10px;">
                    <asp:LinkButton ID="ModifyUser" runat="server" OnClick="ModifyUser_Click">Сделать</asp:LinkButton>
                </li>
                <li>
                    <div class="error-message">
                        <asp:Literal ID="ModifyUserMessage" runat="server" />
                    </div>
                </li>
            </ul>    
        </div>
    <h2>Список пользователей по ролям</h2>    
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

