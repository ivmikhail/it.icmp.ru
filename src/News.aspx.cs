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

public partial class News : System.Web.UI.Page
{
    private Post post;
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
        this.Title += post.Title;
        if (!(CurrentUser.isAuth && CurrentUser.User == post.Author))
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
    /// Загрузка автора нового коммента и капчи(если нужно).
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
        post = Posts.GetById(GetPostId());
        if (post.Id > 0)
        {
            HyperLinkCategory.Text = post.Category.Name;
            HyperLinkCategory.NavigateUrl = "default.aspx?cat=" + post.Category.Id;
            HyperLinkTitle.Text = post.Title;
            HyperLinkTitle.NavigateUrl = "news.aspx?id=" + post.Id;

            desc.Text = post.Description;
            text.Text = post.Text;
            comments_count.Text = post.CommentsCount.ToString();
            date.Text = post.CreateDate.ToString();
            source.Text = post.Source;
            author.Text = post.Author.Nick;
            views.Text = post.Views.ToString();
        } else
        {
            Response.Redirect(FormsAuthentication.DefaultUrl);
        }
    }
    private int GetPostId()
    {
        int id = -1;
        Int32.TryParse(Request.QueryString["id"], out id); 
        return id;
    }

    private void LoadComments()
    {
        List<Comment> comments = Comments.GetByPost(post.Id);
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
            } else { 
                captcha.SetErrorMessageVisible(); 
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
        Comments.Add(comm);
        Response.Redirect("news.aspx?id=" + post.Id + "#comments");
    }
}
