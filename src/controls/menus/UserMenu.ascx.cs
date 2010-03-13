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
	public partial class UserMenu : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				if (CurrentUser.isAuth) {
					User user = CurrentUser.User;
					UserGreetingText.Text = Greeting.Get(user.Login);
					UserRoleText.Text = user.Role.ToString();

                    int newMessagesCount = Message.GetNewCount(user.Id);
                    string cssClass = newMessagesCount > 0 ? " class=\"new-message\" " : "";
                    
                    NewMessagesCountText.Text = String.Format("<span " + cssClass + ">{0}</span>", newMessagesCount);
          			this.Visible = true;
				}
				else {
					this.Visible = false;
				}
			}
		}

		protected void LinkButtonExit_Click(object sender, EventArgs e) {
			CurrentUser.LogOut();
			Response.Redirect("default.aspx");
		}
	}
}
