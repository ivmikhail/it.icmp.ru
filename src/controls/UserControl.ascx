<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserControl.ascx.cs" Inherits="ITCommunity.UserControl" %>
<div id="user_profile">
    <h1>Aloha <asp:Literal ID="LabelUserLogin" runat="server" Text="usernick" />!</h1>
    <ul class="list user-profile">
        <li>
            UserRole - <asp:Literal ID="LabelUserRole" runat="server" Text="userrole" />
        </li>        
        <li>            
            <a href="notepad.aspx" title="���������� ������">�������</a>
        </li>
        <li>
            <asp:Literal ID="MessagesLink" runat="server" />
        </li>
        <li>
            <a href="favorites.aspx" title="������ ������� � �������">���������</a>
        </li>    
        <li>
            <asp:LinkButton ID="LinkButtonExit" runat="server" OnClick="LinkButtonExit_Click">�����</asp:LinkButton>        
        </li>
    </ul>   
</div>
