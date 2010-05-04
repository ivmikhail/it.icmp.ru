using System;
using System.Web.UI;

namespace ITCommunity {
	public partial class LastCommentsMenu : UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				LoadLastComments();
			}
		}

		private void LoadLastComments() {
			LastComments.DataSource = Comment.GetLastComments(Config.GetInt("LastCommentsCount"));
			LastComments.DataBind();
		}
	}
}
