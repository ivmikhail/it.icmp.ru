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

namespace ITCommunity {
	public partial class CommentsList : System.Web.UI.UserControl {
		protected void Page_Load(object sender, EventArgs e) {
		}

		public void DataBind(List<Comment> comments) {
			RepeaterComments.DataSource = comments;
			RepeaterComments.DataBind();
			Visible = (comments.Count > 0);
		}

		protected void RepeaterComments_ItemCommand(object source, RepeaterCommandEventArgs e) {
			if (e.CommandName == "delete") {
				if (CurrentUser.User.Role == ITCommunity.User.Roles.Admin) {
					if (IsPostBack) {
						string commentId = e.CommandArgument.ToString().Split(',')[0];
						string postId = e.CommandArgument.ToString().Split(',')[1];

						Comment.Delete(Convert.ToInt32(commentId));
						Response.Redirect("news.aspx?id=" + postId + "#comments");
					}
				}
			}
		}

		protected void RepeaterComments_ItemDataBound(object sender, RepeaterItemEventArgs e) {

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
				if (CurrentUser.User.Role == ITCommunity.User.Roles.Admin) {
					((LinkButton)e.Item.FindControl("DeleteComment")).Visible = true;
				}
			}
		}
	}
}
