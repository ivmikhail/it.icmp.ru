<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollMenu.ascx.cs" Inherits="ITCommunity.PollMenu" EnableViewState="true"%>

<div id="poll-menu" class="menu-panel">
	<h1>Голосование</h1>

	<h3>
		<asp:Literal ID="LiteralPollTopic" runat="server" />
	</h3>

	<asp:CheckBoxList    ID="CheckBoxListAnswer"    runat="server" />
	<asp:RadioButtonList ID="RadioButtonListAnswer" runat="server" />

	<div class="vote-link-panel">
		<asp:LinkButton ID="LinkButtonVote" runat="server" OnClick="LinkButtonVote_Click">Голосовать</asp:LinkButton>
	</div>

	<asp:Literal ID="UserVotedText" runat="server">
		<div class="message">
			Вы уже голосовали
		</div>
	</asp:Literal>
	
	<div class="note">
		<a href="polls.aspx" title="Посмотреть архив опросов">Архив опросов</a> |
		<a href="pollresult.aspx" title="Смотреть результаты активного опроса">Результаты</a>

		<div>
			<asp:Literal ID="LiteralNote" runat="server" />
		</div>
	</div>

</div>
