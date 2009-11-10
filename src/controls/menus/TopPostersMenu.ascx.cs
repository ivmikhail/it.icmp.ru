using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class TopPostersMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadTopPosters();
			}
		}
		private void LoadTopPosters()
		{
			TopPosters.DataSource = User.GetTopPosters(Global.ConfigNumParam("TopPostersCount"));
			TopPosters.DataBind();
		}

	}
}
