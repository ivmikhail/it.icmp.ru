<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addpoll.aspx.cs" Inherits="ITCommunity.Addpoll" Title="Ykt IT Community | Добавление опроса" %>

<%@ Register Src="~/controls/Pager.ascx"     TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PollsView.ascx" TagName="PollsView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Добавление нового опроса</h1>
    <div id="newpoll">
        <ul class="list">
            <li>
                Вопрос(топик)
            </li>
            <li>            
                <asp:TextBox ID="TextBoxTopic" runat="server" Columns="60"/>
            </li>
            
            <li>
                Варианты ответов(один вариант на одной строке)
            </li>
            <li>            
                <asp:TextBox ID="TextBoxAnswers" runat="server" TextMode="MultiLine" Rows="10" Columns="60" />
            </li>
            
            <li>
            
                Сколько вариантов можно выбрать:
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                    <asp:ListItem Selected="True" Text="Только один" Value="0"/>
                    <asp:ListItem Text="Несколько" Value="1"/>
                </asp:RadioButtonList>
            </li>
            <li>            
                <asp:LinkButton ID="LinkButtonAddPoll" runat="server">Добавить</asp:LinkButton>
            </li>
        </ul>
    </div>
<h1>Архив опросов</h1>
    <div id="polls-container">
        <uc:PollsView id="PollsView" runat="server" />
    </div>
    <div id="pager-container">
        <uc:Pager id="PollsPager" runat="server" />
    </div>
</asp:Content>

