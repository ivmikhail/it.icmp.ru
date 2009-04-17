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
        List<Post> posts = Posts.GetAttached();
        posts.AddRange(Posts.GetPosts(page_num, Global.PostsCount));
        RepeaterPosts.DataSource = posts;
        RepeaterPosts.DataBind();
    }
}
