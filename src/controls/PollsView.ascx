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
              <th>
                 дата начала
              </th>
              <th>
                 дата окончания
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
                    <%# Eval("createdate", "{0:dd MMMM yyyy, HH:mm}")%> 
                </td>
                <td>
                    <%# Eval("enddatestring")%> 
                </td>
                <td>
                    <a href="pollresult.aspx?id=<%# Eval("id") %>" title="Посмотреть результаты">результаты</a>
                </td>
            </tr> 
        </ItemTemplate>
        <FooterTemplate>            
            </tbody>
        </table>
        </FooterTemplate>
</asp:Repeater>