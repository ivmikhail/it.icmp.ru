<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mail.aspx.cs" Inherits="ITCommunity.Mail" Title="Ykt IT Community | Просмотр сообщения" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Просмотр сообщения</h1>
    <ul class="list">
        <li>
            <h3>От кого</h3>
            <asp:Literal ID="Sender" runat="server" />
        </li>
        <li>
            <h3>Заголовок</h3>
            <asp:Literal ID="MessageTitle" runat="server" />
        </li>
        <li>
            <h3>Текст</h3> 
            <asp:Literal ID="MessageText" runat="server" />                
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="DeleteLink" runat="server" OnClick="DeleteLink_Click">Удалить</asp:LinkButton>
            <asp:Literal ID="ReplyLink" runat="server" />
            <a href="mailview.aspx">Вернуться к списку</a>
        </li>
    </ul>
</asp:Content>

