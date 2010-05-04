using System;
using System.Web.UI;

namespace ITCommunity {

	public partial class Mailsend : Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				MessageReceiver.Text = GetReceiver();
				int replyMessageId = getReplyMessageId();
				if (replyMessageId > 0) {
					Message replyMessage = Message.Get(replyMessageId);
					MessageTitle.Text = "RE:" + replyMessage.Title;
				}
			}
		}

		protected void LinkButtonSend_Click(object sender, EventArgs e) {
			//NOTE: bydlo-style code
			ITCommunity.User receiver = ITCommunity.User.Get(MessageReceiver.Text);
			string errors = "";

			if (receiver.Id < 1) {
				errors = "Пользователь с таким логином у нас не живет";
			}
			else if (receiver.Id == CurrentUser.User.Id) {
				errors = "Зачем отправлять сообщение самому себе? Не хватает общения? У нас этого делать нельзя";
			}
			else if (MessageTitle.Text.Trim() == "" || MessageText.Text.Trim() == "") {
				errors = "Заголовок и текст сообщения не могут быть пустыми";
			}

			if (errors == "") {
				Message.Send(receiver.Id, CurrentUser.User.Id, MessageTitle.Text, MessageText.Text);
				Response.Redirect("mailview.aspx?a=output");
			}
			else {
				Errors.Text = "<div class=\"error\">" + errors + "</div>";
			}
		}

		private string GetReceiver() {
			return Request.QueryString["receiver"];
		}

		private int getReplyMessageId() {
			int paramValue = -1;
			Int32.TryParse(Request.QueryString["mid"], out paramValue);
			return paramValue;
		}
	}
}