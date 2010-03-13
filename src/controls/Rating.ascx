<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="ITCommunity.RatingControl" %>

<div id="RatingPanel" runat="server" class="rating">
	<span id="RatingValue" runat="server" class="rating-value">
		<span class="sign-<%# ValueSign %>">
			<%# Value %>
		</span>
	</span>
	<span id="RatingButtons" runat="server" class="rating-buttons" visible="false">
		<asp:LinkButton ID="IncRating" runat="server" OnClick="IncRatingClick" Text="+" ToolTip="Нравится"/>
		|
		<asp:LinkButton ID="DecRating" runat="server" OnClick="DecRatingClick" Text="&ndash;" ToolTip="Не нравится"/>
	</span>
	<span id="RatingMessage" runat="server" class="rating-message">
		<%# Message %>
	</span>
</div>
