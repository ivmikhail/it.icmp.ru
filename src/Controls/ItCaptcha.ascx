<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ItCaptcha" %>
<div class="captcha">
    ������� ���������� ����� �� ������ ������:
    <br />
        <asp:Literal ID="lblQuestion" runat="server" EnableViewState="False">��� �������� C#?</asp:Literal>
        <asp:DropDownList ID="ddlVariants" runat="server" />
        <span class="error-message">
            <asp:Literal ID="lblErrorMessage" runat="server" EnableViewState="False" Visible="false">������������ �����</asp:Literal>
        </span>
        <asp:HiddenField ID="hdnRightAnswer" runat="server" />
</div>