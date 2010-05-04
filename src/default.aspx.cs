using System;
using System.Web.UI;
using System.Collections.Generic;

namespace ITCommunity {

	public partial class Default : Page {

		protected void Page_Load(object sender, EventArgs e) {
			LoadPosts();
		}

		private void LoadPosts() {
			int totalRecords = 0;
			int catId = GetCatId();
			int page = GetPage();
			int postsPerPage = Config.GetInt("PostsCount");

			List<Post> posts;
			if (catId > 0) {
				posts = Post.GetByCategory(page, postsPerPage, catId, ref totalRecords);
			}
			else {
				posts = Post.Get(page, postsPerPage, ref totalRecords);
			}

			PostsList.DataBind(posts, totalRecords, postsPerPage);
		}

		private int GetCatId() {
			int id = -1;
			Int32.TryParse(Request.QueryString["cat"], out id);
			return id;
		}

		private int GetPage() {
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}
	}
}
