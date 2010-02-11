<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="ITCommunity.RatingControl" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
	Namespace="System.Web.UI" TagPrefix="asp" %>

<span class="rating">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<span class="rating-value"><%# Value %></span>
		<asp:LinkButton ID="IncCommentRating" runat="server" CommandName="IncCommentRating" CommandArgument="<%# Id %>" Text="+" />
		|
		<asp:LinkButton ID="IncCommentRating" runat="server" CommandName="DecCommentRating" CommandArgument="<%# Id %>" Text="-" />
	</asp:UpdatePanel>
</span>
