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
	public partial class UserCommentsList : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		public void DataBind(List<Comment> comments, int totalPosts, int postsPerPage)
		{
			CommentsList.DataBind(comments);
			Pager.DataBind(totalPosts, postsPerPage);
		}
	}
}
