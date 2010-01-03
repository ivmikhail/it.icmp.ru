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
using ITCommunity;

namespace ITCommunity
{
	public partial class UserMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				if (CurrentUser.isAuth)
				{
					User user = CurrentUser.User;
					UserGreetingText.Text = Greeting.GetInstance().GetGreeting() + ", " + user.Login + "!";
					UserRoleText.Text = user.Role.ToString();
					NewMessagesCountText.Text = Message.GetNewCount(user.Id).ToString();
					this.Visible = true;
				}
				else
				{
					this.Visible = false;
				}
			}
		}

		protected void LinkButtonExit_Click(object sender, EventArgs e)
		{
			CurrentUser.LogOut();
			Response.Redirect("default.aspx");
		}
	}
}
