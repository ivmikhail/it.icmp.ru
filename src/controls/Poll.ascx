<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Poll.ascx.cs" Inherits="ITCommunity.controls_Poll" EnableViewState="true"%>
<h3>
    <asp:Literal ID="LiteralPollTopic" runat="server" />
</h3>
<div id="poll">
    <asp:CheckBoxList    ID="CheckBoxListAnswer"    runat="server" CellPadding="5" CellSpacing="5" />
    <asp:RadioButtonList ID="RadioButtonListAnswer" runat="server" CellPadding="5" CellSpacing="5"  />
    
    <div class="poll_link">
        <asp:LinkButton ID="LinkButtonVote" runat="server" OnClick="LinkButtonVote_Click" >����������</asp:LinkButton>
        <asp:Literal ID="UserVotedText" runat="server">�� ��� ����������</asp:Literal>
    </div>
    <p class="note">
        <asp:Literal ID="LiteralNote" runat="server"/>
    </p>  
    
    <p class="note">
        <a href="polls.aspx" title="���������� ����� �������">����� �������</a>
        |
        <a href="pollresult.aspx" title="�������� ���������� ��������� ������">����������</a>
    </p>
</div>