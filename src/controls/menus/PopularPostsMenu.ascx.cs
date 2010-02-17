using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ITCommunity
{
	public partial class PopularPostsMenu : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				LoadPopularPosts();
			}
		}
		private void LoadPopularPosts()
		{
			PopularPostsByViews.DataSource = Post.GetTopByViews(Global.ConfigNumParam("PopularPostsPeriod"), Global.ConfigNumParam("PopularPostsCount"));
			PopularPostsByViews.DataBind();

           // PopularPostsByRating.DataSource = Post.GetTopByViews(Global.ConfigNumParam("PopularPostsPeriod"), Global.ConfigNumParam("PopularPostsCount"));
           // PopularPostsByRating.DataBind();
		}
	
	}
}
