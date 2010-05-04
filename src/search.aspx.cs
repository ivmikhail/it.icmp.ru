using System;
using System.Web.UI;
using System.Collections.Generic;

namespace ITCommunity {

	public partial class Search : Page {

		protected void Page_Load(object sender, EventArgs e) {
			TextBoxQuery.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) __doPostBack('" + LinkButtonSearch.UniqueID + "','')");
			if (!IsPostBack) {
				string query = GetQuery();
				if (query != "") {
					LoadPosts(query);
					TextBoxQuery.Text = query;
				}
			}
		}

		protected void LinkButtonSearch_Click(object sender, EventArgs e) {
			Response.Redirect("search.aspx?q=" + TextBoxQuery.Text.Trim());
		}

		private void LoadPosts(string query) {
			int total_records = 0;
			int page = GetPageFromQuery();
			int searchedPostsPerPage = Config.GetInt("PostsCount");
			//List<Post> searchedPosts = Post.Search(page, searchedPostsPerPage, query, ref total_records);
			List<Post> searchedPosts = Post.SearchLucene(query, page, searchedPostsPerPage, ref total_records);
			FindedPosts.DataBind(searchedPosts, total_records, searchedPostsPerPage);

			if (total_records == 0) {
				NotFoundText.Visible = true;
			}
			else {
				NotFoundText.Visible = false;
			}
		}

		private int GetPageFromQuery() {
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}

		private string GetQuery() {
			string res = Request.QueryString["q"];
			return res == null ? "" : res;
		}
	}
}
