using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using OpenFlashChartLib;
using OpenFlashChartLib.Charts;
using OpenFlashChartLib.Controls;

namespace ITCommunity {

	public partial class PollResultPage : Page {

		Poll poll;

		protected void Page_Load(object sender, EventArgs e) {
			int poll_id = GetPollId();
			if (poll_id > 0) {
				poll = Poll.GetById(poll_id);
			}
			else {
				poll = Poll.GetActive();
			}
			OpenFlashChartControl.BuildChart();
			//загрузка сообщений
			LoadMessages();
			//загрузка данных об избирателях
			LoadVoters();
		}

		protected void DrawChart(object sender, DrawChartEventArgs e) {

			Graph graph = new Graph();
			graph.AddElement(GetPieData(poll));
			graph.Title = new Title(poll.Topic);
			graph.Title.Style = "{font-size: 24px; color: #767676;}";
			graph.Bgcolor = "#ffffff";
			// graph.Tooltip = new ToolTip("#label#, #val# of #total#, #percent# of 100%");
			e.Graph = graph;
		}

		private int GetPollId() {
			int poll_id;
			Int32.TryParse(Request.QueryString["id"], out poll_id);
			return poll_id;
		}

		private void LoadMessages() {
			// сообщение, защитан ли голос
			string vote_message = (string)Session["poll_message"];
			if (vote_message != "") {
				PollMessageText.Text = vote_message;
				Session.Remove("poll_message");
			}

			// период голосования 
			VotersCountText.Text = poll.VotesCount.ToString();
			CreateDateText.Text = poll.CreateDate.ToString("dd MMMM yyyy, HH:mm");
			CloseDateText.Text = poll.EndDateString;

			MultiSelectText.Visible = poll.IsMultiSelect;
			NoMultiSelectText.Visible = !poll.IsMultiSelect;

			DeletePollLink.Visible = (CurrentUser.User.Role == ITCommunity.User.Roles.Admin);
		}

		private Pie GetPieData(Poll poll) {
			Pie pie = new Pie();
			foreach (PollAnswer ans in poll.Answers) {
				pie.Values.Add(new PieValue(ans.VotesCount, ans.Text));
			}
			pie.Fontsize = 10;
			pie.Alpha = 0.4;
			pie.Colours = new string[] { "#f00ccc", "#2b00db", "#f29100", "#9cc400", "#d6000", "#b1006a", "#29ce00", "#00cb61" };
			pie.Animate = true;
			return pie;
		}

		private void LoadVoters() {
			if (poll.IsOpen) {
				Answers.DataSource = poll.Answers;
				Answers.DataBind();
			}
			else {
				ClosedPollText.Visible = true;
			}
		}

		protected void Answers_ItemDataBound(object sender, RepeaterItemEventArgs e) {
			Repeater Voters = (Repeater)e.Item.FindControl("Voters");

			Voters.DataSource = ((PollAnswer)e.Item.DataItem).GetUsers();

			Voters.DataBind();
		}

		protected void DeletePollLink_Click(object sender, EventArgs e) {
			int del_id = GetPollId();
			if (del_id > 0) {
				Poll del_poll = Poll.GetById(del_id);
				if (del_poll.Id > 0) {
					Poll.Delete(del_poll.Id);
				}
			}
			else {
				Poll.Delete(Poll.GetActive().Id);
			}
			Response.Redirect("polls.aspx");
		}
	}
}
