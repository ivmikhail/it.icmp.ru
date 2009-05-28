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
            string query = GetQuery();
            if (query != "")
            {
                LoadPosts(query);
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
            FindedPosts.PostSource = Post.Search(page, Global.PostsCount, query, ref total_records);

            FindedPostsPager.Fill("search.aspx", "", "page", page, total_records, Global.PostsCount);
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