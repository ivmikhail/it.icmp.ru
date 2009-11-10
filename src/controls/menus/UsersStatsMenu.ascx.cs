using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class UsersStatsMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadLastRegistered();
				LoadStat();
			}
		}

		private void LoadLastRegistered()
		{
			LastRegistered.DataSource = User.GetLastRegistered(Global.ConfigNumParam("LastRegisteredCount"));
			LastRegistered.DataBind();
		}

		private void LoadStat()
		{
			List<KeyValuePair<string, string>> stats = User.GetStats();
			foreach (KeyValuePair<string, string> stat in stats)
			{
				if (stat.Key == "admins")
				{
					TotalAdmins.Text = stat.Value;
				}
				else if (stat.Key == "posters")
				{
					TotalPosters.Text = stat.Value;
				}
				else
				{
					TotalUsers.Text = stat.Value;
				}
			}
		}
	}
}
