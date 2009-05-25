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

    public partial class News : System.Web.UI.Page
    {
        private Post post;
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            this.Title += post.Title;
            if (CurrentUser.User != post.Author) // пусть пока будет так TODO: приделать чтобы работало через кукисы
            {
                post.UpdateViews();
            }
        }
        private void LoadData()
        {
            LoadPost();
            LoadCommentData();
            LoadComments();
        }
        /// <summary>
        /// «агрузка автора нового коммента и капчи(если нужно).
        /// </summary>
        private void LoadCommentData()
        {
            if (CurrentUser.isAuth)
            {
                author_login.Text = CurrentUser.User.Nick;
            } else
            {
                author_login.Text = "anonymous";
                captcha.Visible = true;
            }
        }
        private void LoadPost()
        {
            post = Post.GetById(GetPostId());
            if (post.Id > 0)
            {
                WritePostCategories(post);
                HyperLinkTitle.Text = post.Title;
                HyperLinkTitle.NavigateUrl = "news.aspx?id=" + post.Id;

                desc.Text = post.Description;
                text.Text = post.Text;
                comments_count.Text = post.CommentsCount.ToString();
                date.Text = post.CreateDate.ToString();
                if (post.Source != "")
                {
                    source.Text = "<a href='" + post.Source + "' target='_blank'>источник</a>";
                }
                author.Text = post.Author.Nick;
                views.Text = post.Views.ToString();

                PostManageControls.Post = post;
            } else
            {
                Response.Redirect(FormsAuthentication.DefaultUrl);
            }
        }
        private void WritePostCategories(Post post)
        {
            string result = String.Empty;
            foreach (Category cat in Category.GetPostCategories(post.Id))
            {
                if (result.Length > 0)
                {
                    result += ", ";
                }
                result += "<a href='default.aspx?cat=" + cat.Id + "'>" + cat.Name + "</a>";
            }
            LinksPostCategories.Text = result;
        }
        private int GetPostId()
        {
            int id = -1;
            Int32.TryParse(Request.QueryString["id"], out id);
            return id;
        }

        private void LoadComments()
        {
            List<Comment> comments = Comment.GetByPost(post.Id);
            RepeaterComments.DataSource = comments;
            RepeaterComments.DataBind();

        }

        protected void LinkButtonAddComment_Click(object sender, EventArgs e)
        {
            if (captcha.Visible)
            {
                if (captcha.IsRightAnswer())
                {
                    AddComment();
                } else
                {
                    captcha.SetErrorMessageVisible();
                    LinkButtonAddComment.Focus();
                }
            } else
            {
                AddComment();
            }
        }

        private void AddComment()
        {
            Comment comm = new Comment();
            comm.Author = CurrentUser.User;
            comm.CreateDate = DateTime.Now;
            comm.Ip = CurrentUser.Ip;
            comm.Post = post;
            comm.Text = Server.HtmlEncode(TextBoxComment.Text);
            Comment.Add(comm);
            Response.Redirect("news.aspx?id=" + post.Id + "#comments");
        }
    }
}