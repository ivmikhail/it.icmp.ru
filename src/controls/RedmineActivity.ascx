<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RedmineActivity.ascx.cs" Inherits="ITCommunity.RedmineActivity" %>
<div id="redmine-activity">
<asp:Repeater ID="ActivityItems" runat="server" EnableViewState="false">
    <ItemTemplate>
        <%# Eval("Author") %> - 
        <a href='<%# Eval("Url") %>' target="_blank"><%# Eval("Title") %></a>
        <br />
    </ItemTemplate>

</asp:Repeater>
</div>
