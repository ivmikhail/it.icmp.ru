using System;

namespace ITCommunity {

	public partial class ItCaptchaList : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				dataBind();
			}
		}

		private void dataBind() {
			this.gridList.DataSource = Database.CaptchaQuestionsList();
			this.gridList.DataBind();
		}
	}
}