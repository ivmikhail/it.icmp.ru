<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addpoll.aspx.cs" Inherits="ITCommunity.Addpoll" Title="Ykt IT Community | ���������� ������" %>

<%@ Register Src="~/controls/Pager.ascx"     TagName="Pager" TagPrefix="uc" %>
<%@ Register Src="~/controls/PollsView.ascx" TagName="PollsView" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>���������� ������ ������</h1>
    <div id="newpoll">
        <ul class="list">
            <li>
                ������(�����)
            </li>
            <li>            
                <asp:TextBox ID="TextBoxTopic" runat="server" Columns="60"/>
            </li>
            
            <li>
                �������� �������(���� ������� �� ����� ������)
            </li>
            <li>            
                <asp:TextBox ID="TextBoxAnswers" runat="server" TextMode="MultiLine" Rows="10" Columns="60" />
            </li>
            
            <li>
            
                ������� ��������� ����� �������:
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" RepeatDirection="Vertical">
                    <asp:ListItem Selected="True" Text="������ ����" Value="0"/>
                    <asp:ListItem Text="���������" Value="1"/>
                </asp:RadioButtonList>
            </li>
            <li>            
                <asp:LinkButton ID="LinkButtonAddPoll" runat="server">��������</asp:LinkButton>
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

