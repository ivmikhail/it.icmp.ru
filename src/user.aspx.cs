using System;
using System.Web.UI;
using System.Collections.Generic;

namespace ITCommunity {

	public partial class UserPage : Page {

		protected void Page_Load(object sender, EventArgs e) {

			if (!IsPostBack) {
				User pageUser = GetPageUser();

				if (pageUser.Id > 0) {

					UserNotFound.Visible = false;
					UserPanel.Visible = true;

					this.Title += " | " + pageUser.Login;
					UserLogin.Text = pageUser.Login;
					SendMessageLink.NavigateUrl = "mailsend.aspx?receiver=" + pageUser.Login;
					SendMessageLink.Visible = (pageUser.Id != CurrentUser.User.Id);

					CommentsLink.Text = "Комментарии(" + pageUser.CommentsCount + ")";
					CommentsLink.NavigateUrl = "user.aspx?login=" + pageUser.Login + "&view=comments";

					PostsLink.Text = "Посты(" + pageUser.PostsCount + ")";
					PostsLink.NavigateUrl = "user.aspx?login=" + pageUser.Login + "&view=posts";

					RegDate.Text = pageUser.CreateDate.ToString("dd MMMM yyyy, HH:mm");
					int totalRecords = 0;
					int page = GetPage();
					int itemsPerPage = Config.GetInt("PostsCount");

					if (GetViewMode() == "posts") {
						PageInfo.Text = "Посты";
						UserCommentsList.Visible = false;
						UserPostsList.Visible = true;
						List<Post> posts = Post.GetByAuthor(page, itemsPerPage, pageUser.Id, ref totalRecords);
						UserPostsList.DataBind(posts, totalRecords, itemsPerPage);
					}
					else {
						PageInfo.Text = "Комментарии";
						UserCommentsList.Visible = true;
						UserPostsList.Visible = false;
						List<Comment> comments = Comment.GetCommentsByAuthor(pageUser.Id, page, itemsPerPage, ref totalRecords);
						UserCommentsList.DataBind(comments, totalRecords, itemsPerPage);
					}
				}
				else {
					UserNotFound.Visible = true;
					UserPanel.Visible = false;
				}
			}
		}

		private User GetPageUser() {
			string UserLogin = Request.QueryString["login"];
			if (UserLogin == "" || UserLogin == null) {
				UserLogin = CurrentUser.User.Login;
			}
			return ITCommunity.User.Get(UserLogin);
		}

		private string GetViewMode() {

			string result = "posts";
			string urlMode = Request.QueryString["view"];
			if (urlMode == "comments") {
				result = urlMode;
			}
			return result;
		}

		private int GetPage() {
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}
	}
}