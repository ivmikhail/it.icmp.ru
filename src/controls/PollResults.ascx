
<%@ Register Assembly="OpenFlashChart" Namespace="OpenFlashChart" TagPrefix="uc" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollResults.ascx.cs" Inherits="ITCommunity.controls_PollResults" %>

<div style="color:Red;">
    <asp:Literal ID="LiteralPollMessage" runat="server" />
</div>

<uc:OpenFlashChartControl ID="OpenFlashChartControl" runat="server" EnableCache="false" Width="100%" LoadingMsg="Загрузка данных..."/>

<div style="text-align:center;">
    <asp:Literal ID="LiteralPollInfo" runat="server" />
</div>
<div style="display:block;">
<h2>
    Кто как голосовал   
</h2>
<asp:Literal ID="LiteralVoters" runat="server" />
</div>