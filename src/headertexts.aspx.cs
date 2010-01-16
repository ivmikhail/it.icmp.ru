using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class HeaderTexts : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				int userId;
				if (Int32.TryParse(Request.QueryString["block"], out userId)) {
					User user = ITCommunity.User.GetById(userId);
					if (user.Role != ITCommunity.User.Roles.Admin) {
						user.CanAddHeaderText = false;
						user.Update();
					} else {
						CantEditAdminError.Visible = true;
					}
				}

				if (Int32.TryParse(Request.QueryString["unblock"], out userId)) {
					User user = ITCommunity.User.GetById(userId);
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
				HeaderTextsRepeater.DataSource = HeaderText.Get(GetPage(), Global.ConfigNumParam("HeaderTextCount"), ref records_count);
				HeaderTextsRepeater.DataBind();

				HeaderTextsPager.DataBind(records_count, Global.ConfigNumParam("HeaderTextCount"));

				BlockedUsersRepeater.DataSource = ITCommunity.User.GetBlocked();
				BlockedUsersRepeater.DataBind();
			}
		}

		private int GetPage() {
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}
	}
}
