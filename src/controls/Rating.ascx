<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Rating.ascx.cs" Inherits="ITCommunity.RatingControl" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" Namespace="System.Web.UI" TagPrefix="asp" %>

<div class="rating">
	<asp:UpdatePanel ID="RatingUpdatePanel" runat="server" UpdateMode="Conditional">
		<ContentTemplate>
			<span id="RatingValue" runat="server" class="rating-value">
				<span class="sign-<%# ValueSign %>">
					<%# Value %>
				</span>
			</span>
			<span id="RatingButtons" runat="server" class="rating-buttons">
				<asp:LinkButton ID="IncRating" runat="server" OnClick="IncRatingClick" Text="+" ToolTip="Нравится"/>
				|
				<asp:LinkButton ID="DecRating" runat="server" OnClick="DecRatingClick" Text="&ndash;" ToolTip="Не нравится"/>
			</span>
			<span id="RatingMessage" runat="server" class="rating-message">
				<%# Message %>
			</span>
		</ContentTemplate>
	</asp:UpdatePanel>
</div>
<div class="clear"></div>
