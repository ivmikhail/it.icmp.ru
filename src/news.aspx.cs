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
		/// Загрузка автора нового коммента и капчи(если нужно).
		/// </summary>
		private void LoadCommentData()
		{
			if (CurrentUser.isAuth)
			{
				userLogin.Text = CurrentUser.User.Nick;
			}
			else
			{
				userLogin.Text = "anonymous";
				captcha.Visible = true;
			}
		}
		private void LoadPost()
		{
			post = Post.GetById(GetPostId());
			if (post.Id > 0)
			{
				EditPostLink.Text = "/ <a href='editpost.aspx?id=" + post.Id + "' title='Отредактировать новость'>редактировать</a> /";
				if (post.IsPostOwner(CurrentUser.User) || CurrentUser.User.Role == ITCommunity.User.Roles.Admin)
				{
					EditPostLink.Visible = DeletePostLink.Visible = true;
				}

				WritePostCategories(post);
				HyperLinkTitle.Text = post.Title;
				HyperLinkTitle.NavigateUrl = "news.aspx?id=" + post.Id;

				desc.Text = post.DescriptionFormatted;
				text.Text = post.TextFormatted;
				comments_count.Text = post.CommentsCount.ToString();
				date.Text = post.CreateDate.ToString("dd MMMM yyyy, HH:mm");
				favorite.Text = post.FavoritesAction;
				if (post.Source != "")
				{
					source.Text = "/ <a href='" + post.Source + "' target='_blank'>источник</a>";
				}
				// Хреново сделал, дублирование
				authorLogin.Text = author.Text = post.Author.Nick;
				views.Text = post.Views.ToString();
			}
			else
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
					result += " , ";
				}
				result += "<a href='default.aspx?cat=" + cat.Id + "' class='category-link'>" + cat.Name + "</a>";
			}
			LinksPostCategories.Text = result;
		}
		private int GetPostId()
		{
			int id = -1;
			Int32.TryParse(Request.QueryString["id"], out id);
			return id;
		}
		private int GetDelCommentId()
		{
			int id = -1;
			Int32.TryParse(Request.QueryString["cid"], out id);
			return id;
		}

		private void LoadComments()
		{
			List<Comment> comments = Comment.GetByPost(GetPostId());
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
				}
				else
				{
					captcha.SetErrorMessageVisible();
					LinkButtonAddComment.Focus();
				}
			}
			else
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
			comm.Text = TextBoxComment.Text;
			comm = Comment.Add(comm);
			Response.Redirect("news.aspx?id=" + post.Id + "#comment-" + comm.Id);
		}

		protected void RepeaterComments_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			if (e.CommandName == "delete")
			{
				if (CurrentUser.User.Role == ITCommunity.User.Roles.Admin)
				{
					if (IsPostBack)
					{
						Comment.Delete(Convert.ToInt32(e.CommandArgument));
						Response.Redirect("news.aspx?id=" + GetPostId() + "#comments");
					}
				}
			}
		}

		protected void RepeaterComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				if (CurrentUser.User.Role == ITCommunity.User.Roles.Admin)
				{
					((LinkButton)e.Item.FindControl("DeleteComment")).Visible = true;
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
