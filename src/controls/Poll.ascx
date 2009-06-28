<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Poll.ascx.cs" Inherits="ITCommunity.controls_Poll" %>
<h3>
    <asp:Literal ID="LiteralPollTopic" runat="server" />
</h3>
<div id="poll">
    <asp:CheckBoxList ID="CheckBoxListAnswer" runat="server" DataTextField="text" DataValueField="id" />
    <asp:RadioButtonList ID="RadioButtonListAnswer" runat="server" DataTextField="text" DataValueField="id" />
    <p class="note">
        <asp:Literal ID="LiteralNote" runat="server"/>
    </p>
    <asp:LinkButton ID="LinkButtonResult" runat="server" OnClick="LinkButtonResult_Click">Результаты</asp:LinkButton>
    <asp:LinkButton ID="LinkButtonVote" runat="server" OnClick="LinkButtonVote_Click">Голосовать</asp:LinkButton>
</div>