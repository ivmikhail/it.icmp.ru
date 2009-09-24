<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Poll.ascx.cs" Inherits="ITCommunity.controls_Poll" EnableViewState="true"%>
<h3>
    <asp:Literal ID="LiteralPollTopic" runat="server" />
</h3>
<div id="poll">
    <asp:CheckBoxList    ID="CheckBoxListAnswer"    runat="server" CellPadding="2" CellSpacing="2" />
    <asp:RadioButtonList ID="RadioButtonListAnswer" runat="server" CellPadding="2" CellSpacing="2"  />
    <p class="note">
        <asp:Literal ID="LiteralNote" runat="server"/>
    </p>
    <p class="note" style="text-align:right;">    
        <a href="polls.aspx" title="Посмотреть архив опросов">архив опросов</a>
    </p>
    <a href="pollresult.aspx" title="Смотреть результаты активного опроса">результаты</a>
    <asp:LinkButton ID="LinkButtonVote" runat="server" OnClick="LinkButtonVote_Click">голосовать</asp:LinkButton>
</div>