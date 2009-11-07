<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notepadadd.aspx.cs" Inherits="ITCommunity.Notepadadd" Title="Ykt IT Community | Добавление заметки" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h1>Добавление записи в блокнот</h1>
    <ul class="list">
        <li>
            <h2>Заголовок</h2>
            <label>
                <asp:TextBox ID="NoteTitle" runat="server" MaxLength="256" Width="100%"/>
            </label>
        </li>
        <li>
            <h2>Текст</h2>
            <label>
                <asp:TextBox ID="NoteText" runat="server" Rows="15" Width="100%" MaxLength="1000" TextMode="MultiLine"/>
            </label>            
        </li>
        <li>
            <asp:ValidationSummary ID="ValidationSummaryNoteAdd" 
                                   runat="server" 
                                   ValidationGroup="ValidateNoteAdd" 
                                   HeaderText="Для добавления записи устраните следующие ошибки"/>
                                   
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" 
                                        runat="server" ControlToValidate="NoteTitle"  
                                        ErrorMessage="Введите заголовок записи" 
                                        ValidationGroup="ValidateNoteAdd"
                                        Display="None" 
                                        />
                                        
            <asp:RequiredFieldValidator ID="RequiredFieldValidatorText" 
                                        runat="server" 
                                        ControlToValidate="NoteText" 
                                        ErrorMessage="Введите текст записи" 
                                        ValidationGroup="ValidateNoteAdd"
                                        Display="None" 
                                        />
        </li>
        <li style="text-align:right;">
            <asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click" ValidationGroup="ValidateNoteAdd">Добавить</asp:LinkButton>
        </li>
    </ul>
   
</asp:Content>

