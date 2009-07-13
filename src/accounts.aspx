<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="accounts.aspx.cs" Inherits="ITCommunity.Accounts" Title="Ykt IT Community | ���������� ��������������" %>
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
    <h1>���������� ��������������</h1>
     <p>���������� 4 ���� �������������:</p>   
     <ul class="list userroles-list">
        <li>
            <b>user</b> - ������� ������������, ����� ����������, �������������� ��� �����, ��������� �������� ������� ������. Readonly ������
        </li>        
        <li>
            <b>poster</b> - ����� ���� "user" + ����� ���������/������������� ���� �������(�������� ����� �� ���������)
        </li>        
        <li>
            <b>admin</b> - ����� ���� "poster" + ����� ��������� ��������, ��������������, ����� �������� �����, ������������� ����� �����. ��� �� ��������� ��������, ������, ����������� ��������
        </li>        
        <li>
            <b>banned</b> - ������� �������(������������ ������)
        </li>
    </ul> 
    <h2>��������� ������������ ���� ������������</h2>       
        <p>������������� �����! ��� ����������� ������������ ������������� ���� poster</p>
        <div id="modify-user">
            <ul class="list">
                <li>
                    <h3>����� ������������</h3> 
                    <asp:TextBox ID="UserLogin" runat="server" Width="100%"/>           
                </li>
                <li>
                     <h3>��������� ����</h3>  
                     <asp:DropDownList ID="DropDownListActions" runat="server" Width="101%"/>      
                </li>                
                <li style="text-align:right; padding-top:10px;">
                    <asp:LinkButton ID="ModifyUser" runat="server" OnClick="ModifyUser_Click">�������</asp:LinkButton>
                </li>
                <li>
                    <div class="error-message">
                        <asp:Literal ID="ModifyUserMessage" runat="server" />
                    </div>
                </li>
            </ul>    
        </div>
    <h2>������ ������������� �� �����</h2>    
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

