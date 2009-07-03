<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mail.aspx.cs" Inherits="ITCommunity.Mail" Title="Ykt IT Community | Просмотр сообщения" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Просмотр сообщения</h1>
    <ul class="list">
        <li>
            <asp:Literal ID="LiteralWho" runat="server" />
        </li>
        <li>
            <h2>Заголовок</h2>
            <asp:Literal ID="MessageTitle" runat="server" />
        </li>
        <li>
            <h2>Текст</h2> 
            <asp:Literal ID="MessageText" runat="server" />    
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="DeleteLink" runat="server" OnClick="DeleteLink_Click">Удалить</asp:LinkButton>
            <asp:Literal ID="ReplyLink" runat="server" />
            <asp:LinkButton ID="LinkButtonBack" runat="server" OnClick="LinkButtonBack_Click">Назад к списку</asp:LinkButton>
        </li>
    </ul>
</asp:Content>

