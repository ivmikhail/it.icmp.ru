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
	public partial class PostsList : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public void DataBind(List<Post> PostSource, int totalPosts, int postsPerPage)
		{
			RepeaterPosts.DataSource = PostSource;
			RepeaterPosts.DataBind();
			Pager.DataBind(totalPosts, postsPerPage);
		}
		
		protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			RepeaterItem item = e.Item;
			if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
			{
				Image AttachedImage = (Image)item.FindControl("AttachedImage");
				Repeater RepeaterPostCategories = (Repeater)item.FindControl("RepeaterPostCategories");
				Post current = (Post)item.DataItem;

				AttachedImage.Visible = current.Attached;

				RepeaterPostCategories.DataSource = Category.GetPostCategories(current.Id);
				RepeaterPostCategories.DataBind();
			}
		}
	}
}
