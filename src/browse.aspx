<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="browse.aspx.cs" Inherits="browse" Title="File browser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblInfo" runat="Server" />
    <asp:Repeater ID="rptFiles" runat="Server" >
        <HeaderTemplate>
          <table>
            <tr>
            <th></th>
              <th>
                Название</th>
              <th>
                Размер</th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td>
              <img src='media/img/<%# Eval("Icon") %>' />
            </td>
            <td>
              <a href='<%# Eval("Link") %>' title='<%# Eval("Description") %>'><%# Eval("Name") %></a>
            </td>
            <td>
              <%# Eval("Size") %>
            </td>
          </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
          <tr>
            <td>
              <img src='media/img/<%# Eval("Icon") %>' />
            </td>
            <td>
              <a href='<%# Eval("Link") %>' title='<%# Eval("Description") %>'><%# Eval("Name") %></a>
            </td>
            <td>
              <%# Eval("Size") %>
            </td>
          </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
          </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

