<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mailsend.aspx.cs" Inherits="ITCommunity.Mailsend" Title="Ykt IT Community | Отправка сообщения" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Отправка сообщения</h1>
    <ul class="list">    
        <li>
             <div class="error-message"><asp:Literal ID="Errors" runat="server" /></div>
        </li>
        <li>
            <h2>Кому</h2>
            <label>
                <asp:TextBox ID="MessageReceiver" runat="server" MaxLength="20" Width="100%"/>
            </label>
        </li>
        <li>
            <h2>Заголовок</h2>
            <label>
               <asp:TextBox ID="MessageTitle" runat="server" MaxLength="50" Width="100%"/>
            </label>
        </li>
        <li>
            <h2>Текст</h2>
            <label>
                <asp:TextBox ID="MessageText" runat="server" Rows="15" Width="100%" MaxLength="1000" TextMode="MultiLine"/>
            </label>            
        </li>
        <li style="text-align:right;">
            <a href='mailview.aspx?'>Отмена</a>
            <asp:LinkButton ID="LinkButtonSend" runat="server" OnClick="LinkButtonSend_Click">Отправить</asp:LinkButton>
        </li>
    </ul>
</asp:Content>

