using System;
using System.Web.UI;

namespace ITCommunity {

	public partial class Addpoll : Page {

		protected void LinkButtonAddPoll_Click(object sender, EventArgs e) {
			if (NewPollIsValid()) {
				string topic = Util.HtmlEncode(TextBoxTopic.Text);
				bool is_multiselect = RadioButtonListMultiselect.Items[1].Selected;
				bool is_open = RadioButtonListIsOpen.Items[1].Selected;

				string[] pre_answers = TextBoxAnswers.Text.Replace("\r", "").Split('\n');
				string answers = "";
				for (int i = 0; i < pre_answers.Length; i++) {
					if (answers != "") {
						answers += ",";
					}
					if (pre_answers[i] != "") {
						answers += Util.HtmlEncode(pre_answers[i]);
					}
				}

				Poll.Add(topic, CurrentUser.User.Id, is_multiselect, is_open, answers);
				Response.Redirect("addpoll.aspx");
			}
		}

		private bool NewPollIsValid() {
			RequiredTopic.Validate();
			RequiredAnswers.Validate();

			return RequiredTopic.IsValid && RequiredAnswers.IsValid;
		}
	}
}
