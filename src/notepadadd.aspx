<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notepadadd.aspx.cs" Inherits="ITCommunity.Notepadadd" Title="Ykt IT Community | ���������� �������" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>���������� ������ � �������</h1>
    <ul class="list">
        <li>
            <h2>���������</h2>
            <label>
                <asp:TextBox ID="NoteTitle" runat="server" MaxLength="256" Width="100%"/>
            </label>
        </li>
        <li>
            <h2>�����</h2>
            <label>
                <asp:TextBox ID="NoteText" runat="server" Rows="15" Width="100%" MaxLength="1000" TextMode="MultiLine"/>
            </label>            
        </li>
        <li>
            <asp:ValidationSummary ID="ValidationSummaryNoteAdd" 
                                   runat="server" 
                                   ValidationGroup="ValidateNoteAdd" 
                                   HeaderText="��� ���������� ������ ��������� ��������� ������"/>
                                   
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" 
                                        runat="server" ControlToValidate="NoteTitle"  
                                        ErrorMessage="������� ��������� ������" 
                                        ValidationGroup="ValidateNoteAdd"
                                        Display="None" 
                                        />
                                        
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorText" 
                                        runat="server" 
                                        ControlToValidate="NoteText" 
                                        ErrorMessage="������� ����� ������" 
                                        ValidationGroup="ValidateNoteAdd"
                                        Display="None" 
                                        />
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click" ValidationGroup="ValidateNoteAdd">��������</asp:LinkButton>
        </li>
    </ul>
   
</asp:Content>

