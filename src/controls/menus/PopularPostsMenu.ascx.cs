using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity {
	public partial class PopularPostsMenu : System.Web.UI.UserControl {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				LoadPopularPosts();
			}
		}

		private void LoadPopularPosts() {
			PopularPostsByViews.DataSource = Post.GetTopByViews(Config.GetInt("PopularPostsPeriod"), Config.GetInt("PopularPostsCount"));
			PopularPostsByViews.DataBind();

			PopularPostsByRating.DataSource = Post.GetTopByRating(Config.GetInt("TopRaitngPostsPeriod"), Config.GetInt("TopRaitngPostsCount"));
			PopularPostsByRating.DataBind();
		}
	}
}
