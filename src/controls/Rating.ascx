<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="ITCommunity.RatingControl" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<span class="rating">
	<asp:UpdatePanel ID="RatingUpdatePanel" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<span class="rating-value"><%# Value %></span>
			<asp:LinkButton ID="IncRating" runat="server" OnClick="IncRatingClick" CommandArgument="<%# EntityId.ToString() + ',' + Type.ToString() %>" Text="+" />
			|
			<asp:LinkButton ID="DecRating" runat="server" OnClick="DecRatingClick"  CommandArgument="<%# EntityId.ToString() + ',' + Type.ToString() %>" Text="-" />
		</ContentTemplate>
	</asp:UpdatePanel>
</span>
