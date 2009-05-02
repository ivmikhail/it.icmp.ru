<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="browse.aspx.cs" Inherits="browse" Title="File browser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblInfo" runat="Server" />
    <asp:Repeater ID="rptFiles" runat="Server" >
        <HeaderTemplate>
          <table>
            <tr>
              <th>
                Name</th>
              <th>
                Description</th>
            </tr>
        </HeaderTemplate>
        <ItemTemplate>
          <tr>
            <td>
              <%# Eval("Name") %>
            </td>
            <td>
              
            </td>
          </tr>
        </ItemTemplate>
        <AlternatingItemTemplate>
          <tr>
            <td>
              <%# Eval("Name") %>
            </td>
            <td>
              
            </td>
          </tr>
        </AlternatingItemTemplate>
        <FooterTemplate>
          </table>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

