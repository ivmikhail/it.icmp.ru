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

public partial class _Default : System.Web.UI.Page
{
    private List<Post> posts;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPosts();
        }
    }
   
    private void LoadPosts()
    {
        if (posts != null)
        {
            posts.Clear();
        }
        int total_records = 0;
        int cat_id = GetCatId();
        int page = GetPage();
        if (cat_id > 0)
        {
            posts = Post.GetPostsByCat(page, Global.PostsCount, cat_id, ref total_records);
        } else
        {
            posts = Post.GetPosts(page, Global.PostsCount, ref total_records);
        }
        RepeaterPosts.DataSource = posts;
        RepeaterPosts.DataBind();

        FillPager(cat_id, total_records, page);

    }

    private void FillPager(int cat_id, int total_records, int current_page)
    {
        NewsPager.PagerPage = "default.aspx";
        NewsPager.CatId = cat_id;
        NewsPager.PageQueryString = "page";
        NewsPager.CurrrentPage = current_page;
        NewsPager.TotalPages = Convert.ToInt32(Math.Ceiling((decimal)total_records/Global.PostsCount));
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
    protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater RepeaterPostCategories = (Repeater)item.FindControl("RepeaterPostCategories");
            Post current = (Post)item.DataItem;
            RepeaterPostCategories.DataSource = Category.GetPostCategrories(current.Id);
            RepeaterPostCategories.DataBind();
        }
    }
}
