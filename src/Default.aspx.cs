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
            int total_records = 0;
            int cat_id = GetCatId();
            int page = GetPage();
            if (cat_id > 0)
            {
                PostsView.PostSource = Post.GetPostsByCat(page, Global.PostsCount, cat_id, ref total_records);
            } else
            {
                PostsView.PostSource = Post.GetPosts(page, Global.PostsCount, ref total_records);
            }

            FillPager(total_records, page, "&cat=" + cat_id);

        }

        private void FillPager(int total_records, int current_pagenum, string pageparams)
        {
            NewsPager.PagerPage = "default.aspx";
            NewsPager.PageParams = pageparams;
            NewsPager.PageQueryString = "page";
            NewsPager.CurrentPage = current_pagenum;
            NewsPager.TotalPages = Convert.ToInt32(Math.Ceiling((decimal)total_records / Global.PostsCount));
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