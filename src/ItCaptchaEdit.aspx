<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ItCaptchaEdit.aspx.cs" Inherits="ITCommunity.ItCaptchaEdit" Title="ItCaptchaEdit" %>
<asp:Content ID="content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:TextBox ID="txtQuestion" runat="server" width="90%" /><br />
    <asp:LinkButton id="lnkSaveQuestion" runat="server" Text="Сохранить" OnClick="lnkSaveQuestion_Click" />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="id">
        <Columns>
            <asp:BoundField DataField="text" HeaderText="Текст" SortExpression="text" />
            <asp:BoundField DataField="isRight" HeaderText="Правильный" SortExpression="isRight" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        DeleteCommand="CaptchaAnswerDelete" DeleteCommandType="StoredProcedure" InsertCommand="CaptchaAnswerAdd"
        InsertCommandType="StoredProcedure" SelectCommand="CaptchaAnswersList" SelectCommandType="StoredProcedure"
        UpdateCommand="CaptchaAnswerUpdate" UpdateCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="-1" Name="id" QueryStringField="id" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="id" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="id" Type="Int32" />
            <asp:Parameter Name="text" Type="String" />
            <asp:Parameter Name="isRight" Type="Byte" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="btnAdd" runat="server" Text="Добавить вариант" OnClick="btnAdd_Click" />
    <br />
    <br />
    <a href="itcaptchalist.aspx">Назад</a>
</asp:Content>

