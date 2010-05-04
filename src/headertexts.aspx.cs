using System;
using System.Collections.Generic;
using System.Web.UI;

namespace ITCommunity {

	public partial class HeaderTexts : Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				int userId;
				if (Int32.TryParse(Request.QueryString["block"], out userId)) {
					User user = ITCommunity.User.Get(userId);
					if (user.Role != ITCommunity.User.Roles.Admin) {
						user.CanAddHeaderText = false;
						user.Update();
					} else {
						CantEditAdminError.Visible = true;
					}
				}

				if (Int32.TryParse(Request.QueryString["unblock"], out userId)) {
					User user = ITCommunity.User.Get(userId);
					user.CanAddHeaderText = true;
					user.Update();
				}

				int headerTextId;
				if (Int32.TryParse(Request.QueryString["del"], out headerTextId)) {
					HeaderText.Delete(headerTextId);
				}

				if (Int32.TryParse(Request.QueryString["end"], out headerTextId)) {
					HeaderText.EndShow(headerTextId);
				}

				int records_count = 0;
				HeaderTextsRepeater.DataSource = HeaderText.Get(GetPage(), Config.GetInt("HeaderTextCount"), ref records_count);
				HeaderTextsRepeater.DataBind();

				HeaderTextsPager.DataBind(records_count, Config.GetInt("HeaderTextCount"));

				List<User> blockedUsers = ITCommunity.User.GetBlocked();
				BlockedUsersRepeater.DataSource = blockedUsers;
				BlockedUsersRepeater.DataBind();
				BlockedUsersRepeater.Visible = (blockedUsers.Count > 0);
			}
		}

		private int GetPage() {
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}
	}
}
