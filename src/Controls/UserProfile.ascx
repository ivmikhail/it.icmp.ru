<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserProfile.ascx.cs" Inherits="Controls_UserProfile" %>
<div id="user_profile">
    <ul>
        <li>
            Aloha <asp:Label ID="LabelUserLogin" runat="server" Text="usernick" />!
        </li>        
        <li>
            UserRole - <asp:Label ID="LabelUserRole" runat="server" Text="userrole" />
        </li>        
        <li>
            <asp:LinkButton ID="LinkButtonNotepad" runat="server">�������</asp:LinkButton> 
        </li>
        <li>
            <asp:LinkButton ID="LinkButtonMessages" runat="server">���������</asp:LinkButton> 
        </li>
        <li>
            <asp:LinkButton ID="LinkButtonFavorite" runat="server">���������</asp:LinkButton> 
        </li>        
        <li>
            <asp:LinkButton ID="LinkButtonExit" runat="server" OnClick="LinkButtonExit_Click">�����</asp:LinkButton>        
        </li>
    </ul>   
</div>
