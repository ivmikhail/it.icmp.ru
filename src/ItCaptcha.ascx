<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ItCaptcha.ascx.cs" Inherits="ItCaptcha" %>
������� ���������� ����� �� ������ ������:<br />
<asp:Label ID="lblQuestion" runat="server" Text="��� �������� C#?" 
    EnableViewState="False"></asp:Label>
<asp:DropDownList ID="ddlVariants" runat="server">
</asp:DropDownList>
<asp:Label ID="lblErrorMessage" runat="server" Text="������������ �����" 
    EnableViewState="False" ForeColor="Red" Visible="False"></asp:Label>
<asp:HiddenField ID="hdnRightAnswer" runat="server" />
