<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ItCaptcha" %>
������� ���������� ����� �� ������ ������:
<p>
    <asp:Label ID="lblQuestion" runat="server" Text="��� �������� C#?" EnableViewState="False" />
    <asp:DropDownList ID="ddlVariants" runat="server" />
    <asp:Label ID="lblErrorMessage" runat="server" Text="������������ �����" EnableViewState="False" ForeColor="Red" Visible="False" />
    <asp:HiddenField ID="hdnRightAnswer" runat="server" />
</p>
