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
			PopularPostsByViews.DataSource = Post.GetTopByViews(Config.Num("PopularPostsPeriod"), Config.Num("PopularPostsCount"));
			PopularPostsByViews.DataBind();

			PopularPostsByRating.DataSource = Post.GetTopByRating(Config.Num("TopRaitngPostsPeriod"), Config.Num("TopRaitngPostsCount"));
			PopularPostsByRating.DataBind();
		}
	}
}
