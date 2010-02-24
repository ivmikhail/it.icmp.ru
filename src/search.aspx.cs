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

	public partial class Search : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			TextBoxQuery.Attributes.Add("onkeypress", "javascript:if (event.keyCode == 13) __doPostBack('" + LinkButtonSearch.UniqueID + "','')");

			if (!IsPostBack)
			{
				string query = GetQuery();
				if (query != "")
				{
					LoadPosts(query);
					TextBoxQuery.Text = query;
				}
			}
		}

		protected void LinkButtonSearch_Click(object sender, EventArgs e)
		{
			LoadPosts(TextBoxQuery.Text);
		}

		private void LoadPosts(string query)
		{
			int total_records = 0;
			int page = GetPage();
			int searchedPostsPerPage = Config.Num("PostsCount");
			List<Post> searchedPosts = Post.Search(page, searchedPostsPerPage, query, ref total_records);
			FindedPosts.DataBind(searchedPosts, total_records, searchedPostsPerPage);

			if (total_records == 0)
			{
				NotFoundText.Visible = true;
			}
			else
			{
				NotFoundText.Visible = false;
			}
		}
		private int GetPage()
		{
			int page_num;
			Int32.TryParse(Request.QueryString["page"], out page_num);
			return page_num == 0 ? 1 : page_num;
		}
		private string GetQuery()
		{
			string res = Request.QueryString["q"];
			return res == null ? "" : res;
		}
	}
}
