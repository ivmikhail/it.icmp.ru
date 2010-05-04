using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {

	public partial class Accounts : Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				FillDropDown();
			}
			FillUsersList();
		}

		private void FillUsersList() {
			admins.Users = ITCommunity.User.GetByRole(ITCommunity.User.Roles.Admin);
			users.Users = ITCommunity.User.GetByRole(ITCommunity.User.Roles.User);
			banned.Users = ITCommunity.User.GetByRole(ITCommunity.User.Roles.Banned);
		}

		private void FillDropDown() {
			DropDownListActions.Items.Add(new ListItem("poster", "2"));
			DropDownListActions.Items.Add(new ListItem("user", "3"));
			DropDownListActions.Items.Add(new ListItem("banned", "4"));
		}
		protected void ModifyUser_Click(object sender, EventArgs e) {
			int int_role = Convert.ToInt32(DropDownListActions.SelectedValue);
			string message = "";
			ITCommunity.User.Roles newrole = (ITCommunity.User.Roles)Enum.ToObject(typeof(ITCommunity.User.Roles), int_role);
			ITCommunity.User mod_user = ITCommunity.User.Get(UserLogin.Text.Trim());

			if (mod_user.Id < 1) {
				message = "<div class='error'>Такой пользователь не зарегистрирован.</div>";
			}
			else if (mod_user.Role == ITCommunity.User.Roles.Admin) {
				message = "<div class='error'>Данный пользователь админ. Над ним нельзя производить никаких действий.</div>";
			}
			else {
				mod_user.Role = newrole;
				mod_user.Update();
				FillUsersList();
				message = "<div class='message'>Изменения вступили в силу.</div>";
			}
			ModifyUserMessage.Text = message;

		}
	}
}
