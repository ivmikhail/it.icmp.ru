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
                LoadLastTopPosters();
                LastTopPostersDays.Text = Global.ConfigNumParam("LastTopUsersDays").ToString();
			}
		}
		private void LoadTopPosters()
		{
			TopPosters.DataSource = User.GetTopPosters(Global.ConfigNumParam("TopPostersCount"));
			TopPosters.DataBind();
		}

        private void LoadLastTopPosters() {
            LastTopPosters.DataSource = User.GetLastTopPosters(Global.ConfigNumParam("TopPostersCount"), Global.ConfigNumParam("LastTopUsersDays"));
            LastTopPosters.DataBind();
        }
	}
}
