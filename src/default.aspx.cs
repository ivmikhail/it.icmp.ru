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
using System.Collections.Generic;
using ITCommunity;

namespace ITCommunity
{

	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			LoadPosts();
		}

		private void LoadPosts()
		{
			int totalRecords = 0;
			int catId = GetCatId();
			int page = GetPage();
			int postsPerPage = Global.ConfigNumParam("PostsCount");

			List<Post> posts;
			if (catId > 0)
			{
				posts = Post.GetByCategory(page, postsPerPage, catId, ref totalRecords);
			}
			else
			{
				posts = Post.Get(page, postsPerPage, ref totalRecords);
			}
			PostsList.DataBind(posts, totalRecords, postsPerPage);
		}

		private int GetCatId()
		{
			int id = -1;
			Int32.TryParse(Request.QueryString["cat"], out id);
			return id;
		}

		private int GetPage()
		{
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}
	}
}
