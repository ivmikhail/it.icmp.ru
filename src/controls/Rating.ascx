<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="ITCommunity.RatingControl" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>

<span class="rating">
	<asp:UpdatePanel ID="RatingUpdatePanel" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<span id="RatingMessage" runat="server" class="rating-message" visible="false">
				<%# Message %>
			</span>
			<span id="RatingValue" runat="server" class="rating-value" visible="false">
				<span class="sign-<%# ValueSign %>">
					<%# Value %>
				</span>
			</span>
			<asp:LinkButton ID="IncRating" runat="server" OnClick="IncRatingClick" Text="+" />
			|
			<asp:LinkButton ID="DecRating" runat="server" OnClick="DecRatingClick" Text="&ndash;" />
		</ContentTemplate>
	</asp:UpdatePanel>
</span>
