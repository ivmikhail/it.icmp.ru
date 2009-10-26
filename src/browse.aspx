<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="browse.aspx.cs" Inherits="ITCommunity.Browse" Title="Ykt IT Community | Файлы" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblInfo" runat="Server" />
    <asp:Repeater ID="rptFiles" runat="Server" >
        <HeaderTemplate>
          <table border="0" width="100%">
            <tr>
              <td>
              <b>&nbsp;</b>
              </td>
              <td>
                <b>Название</b>
              </td>
              <td>
                <b>Размер</b>
              </td>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td>
              <img src='media/img/browser/<%# Eval("Icon") %>' alt='<%# Eval("Name") %>' />
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
              <img src='media/img/browser/<%# Eval("Icon") %>' alt='<%# Eval("Name") %>' />
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

