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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadPosts(1);
        }
    }
    private void LoadPosts(int page_num)
    {
        List<Post> posts = new List<Post>();
        int cat_id = GetCatId();
        if (cat_id > 0)
        {
            posts = Post.GetPostsByCat(page_num, Global.PostsCount, cat_id);
        } else
        {
            posts = Post.GetPosts(page_num, Global.PostsCount);
        }
        RepeaterPosts.DataSource = posts;
        RepeaterPosts.DataBind();
    }
    private int GetCatId()
    {
        int id = -1;
        Int32.TryParse(Request.QueryString["cat"], out id);
        return id;
    }
    protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater RepeaterPostCategories = (Repeater)item.FindControl("RepeaterPostCategories");
            Post current = (Post)item.DataItem;
            RepeaterPostCategories.DataSource = Category.GetPostCategrories(current);
            RepeaterPostCategories.DataBind();
        }
    }
}
