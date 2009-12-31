<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notepadadd.aspx.cs" Inherits="ITCommunity.Notepadadd" Title="Ykt IT Community | Добавление заметки" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Добавление записи в блокнот</h1>

	<label class="textbox-input">
		<span class="label-title">Заголовок</span>
		<asp:TextBox ID="NoteTitle" runat="server" MaxLength="256" />
	</label>

	<label class="textbox-textarea">
		<span class="label-title">Заметка</span>
		<asp:TextBox ID="NoteText" runat="server" Rows="15" MaxLength="1000" TextMode="MultiLine"/>
	</label>

	<asp:ValidationSummary ID="ValidationSummaryNoteAdd" runat="server" ValidationGroup="ValidateNoteAdd"
		HeaderText="Для добавления записи устраните следующие ошибки:"/>
	<asp:RequiredFieldValidator ID="RequiredFieldValidatorTitle" runat="server" ControlToValidate="NoteTitle"
		ErrorMessage=" - введите заголовок записи" ValidationGroup="ValidateNoteAdd" Display="None" />
	<asp:RequiredFieldValidator ID="RequiredFieldValidatorText" runat="server" ControlToValidate="NoteText"
		ErrorMessage="- введите текст записи" ValidationGroup="ValidateNoteAdd" Display="None" />

	<div class="big-button">
		<asp:LinkButton ID="LinkButtonAdd" runat="server" OnClick="LinkButtonAdd_Click" ValidationGroup="ValidateNoteAdd">Добавить</asp:LinkButton>
	</div>
</asp:Content>

