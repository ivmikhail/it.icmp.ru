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
        
    <h2>������� ����-������ ��� ������</h2>    
        <div id="modify-user">
            <ul class="list">
                <li>
                    ����� ������������ <asp:TextBox ID="UserLogin" runat="server" />           
                </li>
                <li>
                     ��� ����� <asp:DropDownList ID="DropDownListActions" runat="server" />      
                </li>                
                <li style="text-align:right">
                    <asp:LinkButton ID="ModifyUser" runat="server" OnClick="ModifyUser_Click">�������</asp:LinkButton>
                </li>
                <li>
                    <div class="error-message">
                        <asp:Literal ID="ModifyUserMessage" runat="server" />
                    </div>
                </li>
            </ul>    
        </div>
    <h2>������ �������������</h2>    
        <div id="accounts-tabs">
	        <ul class="tabSet">
		        <li class="off"><a>��������������</a></li>
		        <li class="off"><a>�������</a></li>
		        <li class="off"><a>����������</a></li>
	        </ul>
	    
            <div class="panelSet">
	            <div class="panel">
	                <uc:UserList id="admins" runat="server" />
	            </div>
	            <div class="panel">
	                <uc:UserList id="posters" runat="server" />
	            </div>
	            <div class="panel">
	                <uc:UserList id="banned" runat="server" />
	            </div>
            </div> 
        </div>
</asp:Content>

