<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="ITCommunity.UserProfile" %>
<div id="user_profile">
<h2>Aloha <asp:Literal ID="LabelUserLogin" runat="server" Text="usernick" />!</h2>
    <ul class="list">
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
            <a href="editpost.aspx" title="�������� ������">�������� ����</a>
        </li>  
        <li>
            <asp:LinkButton ID="LinkButtonExit" runat="server" OnClick="LinkButtonExit_Click">�����</asp:LinkButton>        
        </li>
    </ul>   
</div>
