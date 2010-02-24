using System;
using System.Web.UI;

namespace ITCommunity {
	public partial class AdminMenu : UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			this.Visible = (CurrentUser.User.Role == User.Roles.Admin);
		}
	}
}
