<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addpoll.aspx.cs" Inherits="ITCommunity.Addpoll" Title="Ykt IT Community | ���������� ������" %>

<%@ Register Src="~/controls/Pager.ascx"     TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PollsView.ascx" TagName="PollsView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>���������� ������ ������</h1>
    <div id="newpoll">
        <ul class="list">
            <li>
                <h2>������(�����)</h2>
                <asp:TextBox ID="TextBoxTopic" runat="server" Width="100%"/>
            </li>
            
            <li>
                <h2>�������� �������(���� ������� �� ����� ������)</h2>
                <asp:TextBox ID="TextBoxAnswers" runat="server" TextMode="MultiLine" Rows="10" Width="100%" />
            </li>            
            <li>            
                <h2>������� ��������� ����� �������</h2>
                <asp:RadioButtonList ID="RadioButtonListMultiselect" runat="server" RepeatDirection="horizontal">
                    <asp:ListItem Selected="True" Text="������ ����" Value="0"/>
                    <asp:ListItem Text="���������" Value="1"/>
                </asp:RadioButtonList>     
            </li>
            <li>            
                <h2>��� ������</h2>
                <asp:RadioButtonList ID="RadioButtonListIsOpen" runat="server" RepeatDirection="horizontal">
                    <asp:ListItem Selected="True" Text="��������" Value="0"/>
                    <asp:ListItem Text="��������" Value="1"/>
                </asp:RadioButtonList>       
            </li>
            <li class="big-button">            
                <asp:LinkButton ID="LinkButtonAddPoll" runat="server" OnClick="LinkButtonAddPoll_Click">��������</asp:LinkButton>
            </li>
            <li>
            <asp:ValidationSummary ID="ValidationSummaryAddpoll" runat="server" ValidationGroup="ValidatePoll" DisplayMode="List"  />
        
                <asp:RequiredFieldValidator     ID="RequiredTopic" 
                                                runat="server" 
                                                ControlToValidate="TextBoxTopic"
                                                ErrorMessage="������� �������� ������." 
                                                Display="None" 
                                                ValidationGroup="ValidatePoll" />
                                                
                 <asp:RequiredFieldValidator    ID="RequiredAnswers" 
                                                runat="server" 
                                                ControlToValidate="TextBoxAnswers"
                                                ErrorMessage="������� �������� ������." 
                                                Display="None" 
                                                ValidationGroup="ValidatePoll" />
            </li>
        </ul>
    </div>
<h1>����� �������</h1>
    <div id="polls-container">
        <uc:PollsView id="PollsView" runat="server" />
    </div>
    <div id="pager-container">
        <uc:Pager id="PollsPager" runat="server" />
    </div>
</asp:Content>

