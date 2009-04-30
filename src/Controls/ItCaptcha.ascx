<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ITCommunity.ItCaptcha" %>
<div class="captcha">
    Введите пожалуйста ответ на данный вопрос:
    <br />
        <asp:Literal ID="lblQuestion" runat="server" EnableViewState="False">Кто придумал C#?</asp:Literal>
        <asp:DropDownList ID="ddlVariants" runat="server" />
        <span class="error-message">
            <asp:Literal ID="lblErrorMessage" runat="server" EnableViewState="False" Visible="false">Неправильный ответ</asp:Literal>
        </span>
        <asp:HiddenField ID="hdnRightAnswer" runat="server" />
</div>