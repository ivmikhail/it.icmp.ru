<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addpoll.aspx.cs" Inherits="ITCommunity.Addpoll" Title="Ykt IT Community | Добавление опроса" %>

<%@ Register Src="~/controls/Pager.ascx"     TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PollsView.ascx" TagName="PollsView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Добавление нового опроса</h1>
    <div id="newpoll">
        <ul class="list">
            <li>
                <h2>Вопрос(топик)</h2>
                <asp:TextBox ID="TextBoxTopic" runat="server" Width="100%"/>
            </li>
            
            <li>
                <h2>Варианты ответов(один вариант на одной строке)</h2>
                <asp:TextBox ID="TextBoxAnswers" runat="server" TextMode="MultiLine" Rows="10" Width="100%" />
            </li>            
            <li>            
                <h2>Сколько вариантов можно выбрать</h2>
                <asp:RadioButtonList ID="RadioButtonListMultiselect" runat="server" RepeatDirection="horizontal">
                    <asp:ListItem Selected="True" Text="только один" Value="0"/>
                    <asp:ListItem Text="несколько" Value="1"/>
                </asp:RadioButtonList>     
            </li>
            <li>            
                <h2>Тип опроса</h2>
                <asp:RadioButtonList ID="RadioButtonListIsOpen" runat="server" RepeatDirection="horizontal">
                    <asp:ListItem Selected="True" Text="закрытый" Value="0"/>
                    <asp:ListItem Text="открытый" Value="1"/>
                </asp:RadioButtonList>       
            </li>
            <li class="big-button">            
                <asp:LinkButton ID="LinkButtonAddPoll" runat="server" OnClick="LinkButtonAddPoll_Click">добавить</asp:LinkButton>
            </li>
            <li>
            <asp:ValidationSummary ID="ValidationSummaryAddpoll" runat="server" ValidationGroup="ValidatePoll" DisplayMode="List"  />
        
                <asp:RequiredFieldValidator     ID="RequiredTopic" 
                                                runat="server" 
                                                ControlToValidate="TextBoxTopic"
                                                ErrorMessage="Введите название опроса." 
                                                Display="None" 
                                                ValidationGroup="ValidatePoll" />
                                                
                 <asp:RequiredFieldValidator    ID="RequiredAnswers" 
                                                runat="server" 
                                                ControlToValidate="TextBoxAnswers"
                                                ErrorMessage="Введите варианты ответа." 
                                                Display="None" 
                                                ValidationGroup="ValidatePoll" />
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

