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
			PopularPosts.DataSource = Post.GetTop(Global.ConfigNumParam("PopularPostsPeriod"), Global.ConfigNumParam("PopularPostsCount"));
			PopularPosts.DataBind();
		}
	
	}
}
