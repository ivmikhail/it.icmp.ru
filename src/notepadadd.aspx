<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notepadadd.aspx.cs" Inherits="ITCommunity.Notepadadd" Title="Ykt IT Community | ���������� �������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>���������� ������ � �������</h1>
    <ul class="list">
        <li>
            <h2>���������</h2>
            <label>
                <asp:TextBox ID="NoteTitle" runat="server" MaxLength="20" Width="100%"/>
            </label>
        </li>
        <li>
            <h2>�����</h2>
            <label>
                <asp:TextBox ID="NoteText" runat="server" Rows="15" Width="100%" MaxLength="1000" TextMode="MultiLine"/>
            </label>            
        </li>
        <li>
             <div class="error-message"><asp:Literal ID="Errors" runat="server" /></div>
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click" >��������</asp:LinkButton>
        </li>
    </ul>
</asp:Content>

