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

namespace ITCommunity {
	public partial class AddHeaderText : System.Web.UI.Page {
        protected int PostsCount;
        protected int HeaderTextMaxLength = Global.ConfigNumParam("HeaderTextMaxLength");
        protected int HeaderTextShowingHours = Global.ConfigNumParam("HeaderTextShowingHours");
		protected void Page_Load(object sender, EventArgs e) {
			TextLengthError.Visible = UserBlockedErrorPanel.Visible = ErrorPanel.Visible = MessageText.Visible = false;
			if (!IsPostBack) {

				User current = CurrentUser.User;
				if (!current.CanAddHeaderText) {
					UserBlockedErrorPanel.Visible = true;
				}
				else if (!current.AbleToAddHeaderText()) {
					PostsCount = Global.ConfigNumParam("HeaderTextPostsCount") - current.HeaderTextCounter;
					//PostsCountText.Text = count.ToString();
					ErrorPanel.Visible = true;
				}
			}
		}

		protected void LinkButtonAddHeaderText_Click(object sender, EventArgs e) {
			if (IsDataValid()) {
				User current = CurrentUser.User;
				if (!current.AbleToAddHeaderText())
					return;
				string topic = Util.HtmlEncode(TextBoxText.Text);
				HeaderText.Add(current.Id, topic);
				current.HeaderTextCounter = 0;
				current.Update();
				TextBoxText.Text = "";
				MessageText.Visible = true;
			}
		}

		private bool IsDataValid() {
			RequiredText.Validate();
			if (TextBoxText.Text.Length > Global.ConfigNumParam("HeaderTextMaxLength")) {
				TextLengthError.Visible = true;
				return false;
			}

			return RequiredText.IsValid;
		}
	}
}
