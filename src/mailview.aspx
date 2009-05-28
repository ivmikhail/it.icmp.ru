<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="mailview.aspx.cs" Inherits="ITCommunity.Mailview" Title="Ykt IT Community | Ваши сообщения" %>
<%@ Register Src="~/controls/Pager.ascx" TagName="Pager" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<a href='mailsend.aspx'>Написать</a>
<a href='mailview.aspx'>Входящие</a>
<a href='mailview.aspx?a=output'>Исходящие</a>

<h1><asp:Literal ID="ListTitle" runat="server" /></h1>
    <asp:Repeater ID="RepeaterMessages" runat="server" OnItemDataBound="RepeaterMessages_ItemDataBound">
        <HeaderTemplate>
            <ul id="mail-list" class="list">
        </HeaderTemplate>
        <ItemTemplate>
            <li>
                <div class="message"> 
                    <h2>
                       <a href='mail.aspx?id=<%# Eval("id")%>'><%# Eval("title")%></a>     
                    </h2>
                    <div class="message-info">
                       <%# Eval("CreateDate")%> / <asp:Literal ID="Who" runat="server" />
                    </div>
                <div>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
    
    <uc:Pager id="MessagePager" runat="server" />
</asp:Content>

