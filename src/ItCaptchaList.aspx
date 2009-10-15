<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItCaptchaList.aspx.cs" Inherits="ITCommunity.ItCaptchaList" Title="ItCaptchaList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="gridList" runat="server" AutoGenerateColumns="false" DataKeyNames="ID" 
        AllowPaging="true" PagerSettings-Mode="NumericFirstLast" PageSize="30"
        BorderWidth="1" 
        >
        <Columns>
            <asp:BoundField DataField="text" HeaderText="������" />
            <asp:HyperLinkField Text="��������" DataNavigateUrlFields="ID" DataNavigateUrlFormatString="~/ItCaptchaEdit.aspx?id={0}"/>
        </Columns>
    </asp:GridView>
    <asp:HyperLink ID="hplAdd" runat="server" Text="��������" NavigateUrl="~/ItCaptchaEdit.aspx?new=1" EnableViewState="false" />
</asp:Content>

