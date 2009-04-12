<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ItCaptcha" %>
Введите пожалуйста ответ на данный вопрос:
<p>
    <asp:Label ID="lblQuestion" runat="server" Text="Кто придумал C#?" EnableViewState="False" />
    <asp:DropDownList ID="ddlVariants" runat="server" />
    <asp:Label ID="lblErrorMessage" runat="server" Text="Неправильный ответ" EnableViewState="False" ForeColor="Red" Visible="False" />
    <asp:HiddenField ID="hdnRightAnswer" runat="server" />
</p>
