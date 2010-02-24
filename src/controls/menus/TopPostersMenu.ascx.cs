using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class TopPostersMenu : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				LoadTopPosters();
				LoadLastTopPosters();
				LastTopPostersDays.Text = Config.Num("LastTopUsersDays").ToString();
			}
		}
		private void LoadTopPosters() {
			TopPosters.DataSource = User.GetTopPosters(Config.Num("TopPostersCount"));
			TopPosters.DataBind();
		}

		private void LoadLastTopPosters() {
			LastTopPosters.DataSource = User.GetLastTopPosters(Config.Num("TopPostersCount"), Config.Num("LastTopUsersDays"));
			LastTopPosters.DataBind();
		}
	}
}
