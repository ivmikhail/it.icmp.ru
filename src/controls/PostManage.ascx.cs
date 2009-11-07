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
    public partial class PostManage : System.Web.UI.UserControl
    {
        private Post post;
        public Post Post
        {
            set
            {
                post = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EditPostLink.Text = "<a href='editpost.aspx?id=" + post.Id + "' title='Отредактировать новость'>редактировать</a>";
                if (post.IsPostOwner(CurrentUser.User) || CurrentUser.User.Role == ITCommunity.User.Roles.Admin)
                {
                    PostManager.Visible = true;
                }
            }
        }
        protected void DeletePost_Click(object sender, EventArgs e)
        {
            Post.Delete(post);
            Response.Redirect("default.aspx");
        }
}
}
