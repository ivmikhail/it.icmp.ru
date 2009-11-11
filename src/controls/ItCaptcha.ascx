<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ITCommunity.ItCaptcha" %>

<div class="captcha">
	<label class="dropdown-list-select">
	    <span class="note">
			Введите пожалуйста ответ на данный вопрос, мы должны убедиться что вы действительно имеете отношения с IT :)
		</span>
		<span class="label-title">IT-captcha: <asp:Literal ID="QuestionText" runat="server" EnableViewState="False" /></span>
		<asp:DropDownList ID="VariantsList" runat="server" />
	</label>

	<asp:Literal ID="lblErrorMessage" runat="server" EnableViewState="False" Visible="false">
		<div class="error">
			IT-captcha: Неправильный ответ
		</div>
	</asp:Literal>

	<asp:HiddenField ID="RightAnswer" runat="server" />
</div>
