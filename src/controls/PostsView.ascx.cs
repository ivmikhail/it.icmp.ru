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
        public List<Post> PostSource
        {
            set
            {
                RepeaterPosts.DataSource = value;
                RepeaterPosts.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
                
        }
        protected void RepeaterPosts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterPostCategories = (Repeater)item.FindControl("RepeaterPostCategories");
                Post current = (Post)item.DataItem;

                RepeaterPostCategories.DataSource = Category.GetPostCategories(current.Id);
                RepeaterPostCategories.DataBind();

            }
        }
    }
}