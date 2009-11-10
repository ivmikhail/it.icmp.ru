<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="addpoll.aspx.cs" Inherits="ITCommunity.Addpoll" Title="Ykt IT Community | Добавление опроса" %>

<%@ Register Src="~/controls/PollsList.ascx" TagName="PollsList" TagPrefix="uc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<h1>Добавление нового опроса</h1>

	<label class="textbox-input">
		<h3>Вопрос (топик)</h3>
		<asp:TextBox ID="TextBoxTopic" runat="server" />
	</label>

	<label class="textbox-textarea">
		<h3>Варианты ответов (один вариант на одной строке)</h3>
		<asp:TextBox ID="TextBoxAnswers" runat="server" TextMode="MultiLine" Rows="10" />
	</label>

	<label class="radiobutton-list-input">
		<h3>Сколько вариантов можно выбрать</h3>
		<asp:RadioButtonList ID="RadioButtonListMultiselect" runat="server" RepeatDirection="horizontal">
			<asp:ListItem Selected="True" Text="только один" Value="0"/>
			<asp:ListItem Text="несколько" Value="1"/>
		</asp:RadioButtonList>
	</label>

	<label class="radiobutton-list-input">
		<h3>Тип опроса</h3>
		<asp:RadioButtonList ID="RadioButtonListIsOpen" runat="server" RepeatDirection="horizontal">
			<asp:ListItem Selected="True" Text="закрытый" Value="0"/>
			<asp:ListItem Text="открытый" Value="1"/>
		</asp:RadioButtonList>
	</label>

	<div class="big-button">
		<asp:LinkButton ID="LinkButtonAddPoll" runat="server" OnClick="LinkButtonAddPoll_Click">добавить</asp:LinkButton>
	</div>

	<asp:ValidationSummary ID="ValidationSummaryAddpoll" runat="server" ValidationGroup="ValidatePoll" DisplayMode="List"  />
	<asp:RequiredFieldValidator ID="RequiredTopic" runat="server" ControlToValidate="TextBoxTopic"
		ErrorMessage="Введите название опроса." Display="None" ValidationGroup="ValidatePoll" />
	 <asp:RequiredFieldValidator ID="RequiredAnswers" runat="server" ControlToValidate="TextBoxAnswers"
		ErrorMessage="Введите варианты ответа." Display="None" ValidationGroup="ValidatePoll" />

	<uc:PollsList id="PollsList" runat="server" />
</asp:Content>
