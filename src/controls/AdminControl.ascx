<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminControl.ascx.cs" Inherits="ITCommunity.AdminControl" %>

<h1>���-������ �������</h1>         
<ul class="list admin-panel">
    <li>
       <asp:HyperLink ID="AddPostLink" runat="server" NavigateUrl="~/editpost.aspx">�������� ����</asp:HyperLink>
    </li> 
    <li>
       <asp:HyperLink ID="ManageAccounts" runat="server" NavigateUrl="~/accounts.aspx">������������</asp:HyperLink>
    </li>
    <li>
        <asp:HyperLink ID="AddPollLink" runat="server" NavigateUrl="~/addpoll.aspx">�����������</asp:HyperLink>
    </li>     
    <li>
        <asp:HyperLink ID="SiteStructureLink" runat="server" NavigateUrl="~/structure.aspx">����/���������</asp:HyperLink>
    </li>       
    <li>
        <a href="#" title="">�����</a>
    </li> 
</ul>   
