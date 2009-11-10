using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using OpenFlashChart;

namespace ITCommunity
{
	public partial class PollResultPage : System.Web.UI.Page
	{
		OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
		Poll poll;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				poll = new Poll();
				int poll_id = GetPollId();
				if (poll_id > 0)
				{
					poll = Poll.GetById(poll_id);
				}
				else
				{
					poll = Poll.GetActive();
				}

				//загрузка опроса, первое что должно быть сделано
				LoadPollData();
				//загрузка сообщений
				LoadMessages();
				//загрузка данных об избирателях
				LoadVoters();
			}
		}

		private int GetPollId()
		{
			int poll_id;
			Int32.TryParse(Request.QueryString["id"], out poll_id);
			return poll_id;
		}

		private void LoadPollData()
		{
			chart.AddElement(GetPieData(poll));
			chart.Title = new Title(poll.Topic);
			chart.Title.Style = "{font-size: 24px; color: #767676;}";

			OpenFlashChartControl.EnableCache = false;
			chart.Bgcolor = "#ffffff";
			OpenFlashChartControl.Chart = chart;
		}

		private void LoadMessages()
		{
			// сообщение, защитан ли голос
			string vote_message = (string)Session["poll_message"];
			if (vote_message != "")
			{
				PollMessageText.Text = vote_message;
				Session.Remove("poll_message");
			}

			// период голосования 
			CreateDateText.Text = poll.CreateDate.ToString("dd MMMM yyyy, HH:mm");
			CloseDateText.Text = poll.EndDateString;

			MultiSelectText.Visible = poll.IsMultiSelect;
			NoMultiSelectText.Visible = !poll.IsMultiSelect;

			DeletePollLink.Visible = (CurrentUser.User.Role == ITCommunity.User.Roles.Admin);
		}

		private OpenFlashChart.Pie GetPieData(Poll poll)
		{
			OpenFlashChart.Pie pie = new OpenFlashChart.Pie();

			List<PieValue> values = new List<PieValue>();
			foreach (PollAnswer ans in poll.Answers)
			{
				values.Add(new PieValue(ans.VotesCount, ans.Text));
			}
			pie.Values = values;
			pie.FontSize = 10;
			pie.Alpha = .5;
			pie.Colours = new string[] { "#f00ccc", "#2b00db", "#f29100", "#9cc400", "#d6000", "#b1006a", "#29ce00", "#00cb61" };
			pie.Tooltip = "#label#, #val# of #total#, #percent# of 100%";
			return pie;
		}

		private void LoadVoters()
		{
			if (poll.IsOpen)
			{
				Answers.DataSource = poll.Answers;
				Answers.DataBind();
			}
			else
			{
				ClosedPollText.Visible = true;
			}
		}

		protected void Answers_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Repeater Voters = (Repeater)e.Item.FindControl("Voters");

			Voters.DataSource = ((PollAnswer)e.Item.DataItem).GetUsers();

			Voters.DataBind();
		}

		protected void DeletePollLink_Click(object sender, EventArgs e)
		{
			int del_id = GetPollId();
			if (del_id > 0)
			{
				Poll del_poll = Poll.GetById(del_id);
				if (del_poll.Id > 0)
				{
					Poll.Delete(del_poll.Id);
				}
			}
			Response.Redirect("polls.aspx");
		}
	}
}
