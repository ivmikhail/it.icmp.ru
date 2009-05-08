<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserList.ascx.cs" Inherits="ITCommunity.controls_UserList" %>
<asp:Repeater ID="RepeaterUsers" runat="server">
    <HeaderTemplate>
        <table class="user-list">
            <thead>
              <th>
                login
              </th>
              <th>
                email
              </th>
            </thead>
            <tbody>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
              <%# Eval("nick") %>
            </td>
            <td>
              <%# Eval("email") %>
            </td>
        </tr>        
    </ItemTemplate>
    <FooterTemplate>
        </tbody>
    </table>
    </FooterTemplate>
</asp:Repeater>
