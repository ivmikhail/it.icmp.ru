<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addpoll.aspx.cs" Inherits="ITCommunity.Addpoll" Title="Ykt IT Community | Добавление опроса" %>

<%@ Register Src="~/controls/PollsList.ascx" TagName="PollsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Добавление нового опроса</h1>

	<label class="textbox-input">
		<span class="label-title">Вопрос (топик)</span>
		<asp:TextBox ID="TextBoxTopic" runat="server" />
	</label>

	<label class="textbox-textarea">
		<span class="label-title">Варианты ответов (один вариант на одной строке)</span>
		<asp:TextBox ID="TextBoxAnswers" runat="server" TextMode="MultiLine" Rows="6" />
	</label>

	<div class="radiobutton-list-input">
		<span class="label-title">Сколько вариантов можно выбрать</span>
		<asp:RadioButtonList ID="RadioButtonListMultiselect" runat="server" RepeatDirection="horizontal">
			<asp:ListItem Selected="True" Text="только один" Value="0"/>
			<asp:ListItem Text="несколько" Value="1"/>
		</asp:RadioButtonList>
	</div>

	<div class="radiobutton-list-input">
		<span class="label-title">Тип опроса</span>
		<asp:RadioButtonList ID="RadioButtonListIsOpen" runat="server" RepeatDirection="horizontal">
			<asp:ListItem Selected="True" Text="закрытый" Value="0"/>
			<asp:ListItem Text="открытый" Value="1"/>
		</asp:RadioButtonList>
	</div>

	<asp:ValidationSummary ID="ValidationSummaryAddpoll" runat="server" ValidationGroup="ValidatePoll" DisplayMode="List" CssClass="error" />
	<asp:RequiredFieldValidator ID="RequiredTopic" runat="server" ControlToValidate="TextBoxTopic"
		ErrorMessage="Введите название опроса." Display="None" ValidationGroup="ValidatePoll" />
	 <asp:RequiredFieldValidator ID="RequiredAnswers" runat="server" ControlToValidate="TextBoxAnswers"
		ErrorMessage="Введите варианты ответа." Display="None" ValidationGroup="ValidatePoll" />

	<div class="big-button">
		<asp:LinkButton ID="LinkButtonAddPoll" runat="server" OnClick="LinkButtonAddPoll_Click">добавить</asp:LinkButton>
	</div>

	<div class="panel">
		<uc:PollsList id="PollsList" runat="server" />
	</div>
</asp:Content>
