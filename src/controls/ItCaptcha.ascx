<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ITCommunity.ItCaptcha" %>
<div class="captcha">
    <p class="note">
        Введите пожалуйста ответ на данный вопрос, мы должны убедиться что вы действительно имеете отношения с IT :)
    </p>
    <asp:Literal ID="lblQuestion" runat="server" EnableViewState="False">Кто придумал C#?</asp:Literal>
    <asp:DropDownList ID="ddlVariants" runat="server" />
    <span class="error-message">
         <asp:Literal ID="lblErrorMessage" runat="server" EnableViewState="False" Visible="false">Неправильный ответ</asp:Literal>
    </span>
    <asp:HiddenField ID="hdnRightAnswer" runat="server" />
</div>