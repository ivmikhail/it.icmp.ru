<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollsView.ascx.cs" Inherits="ITCommunity.controls_PollsView"%>

<asp:Repeater ID="RepeaterPolls" runat="server" >
        <HeaderTemplate>
           <table class="user-list">
            <thead>
              <th>
                topic
              </th>
              <th>
                 voters
              </th>
              <th></th>
            </thead>
            <tbody>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <%# Eval("topic")%>
                </td>
                <td>
                    <%# Eval("votescount") %>
                </td>
                <td>
                    <a href="addpoll.aspx?del='<%# Eval("id")%>'" title="удалить">удалить</a>
                </td>
            </tr> 
        </ItemTemplate>
        <FooterTemplate>            
            </tbody>
        </table>
        </FooterTemplate>
</asp:Repeater>