
<%@ Register Assembly="OpenFlashChart" Namespace="OpenFlashChart" TagPrefix="uc" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollResults.ascx.cs" Inherits="ITCommunity.controls_PollResults" %>

<div style="color:Red;">
    <asp:Literal ID="LiteralPollMessage" runat="server" />
</div>

<uc:OpenFlashChartControl ID="OpenFlashChartControl" runat="server" EnableCache="false" Width="100%" LoadingMsg="�������� ������..."/>

<div style="text-align:center;">
    <asp:Literal ID="LiteralMulstiselect" runat="server" />
</div>

<h2>
    ��� ��� ���������   
</h2>
<asp:Literal ID="LiteralVoters" runat="server" />