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
    public partial class controls_PostsView : System.Web.UI.UserControl
    {
        private List<Post> posts;

        public List<Post> PostSource
        {
            get
            {
                return posts;
            }
            set
            {
                posts = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RepeaterPosts.DataSource = posts;
            RepeaterPosts.DataBind();

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
}