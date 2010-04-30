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

namespace ITCommunity {

	public partial class News : System.Web.UI.Page {
		private Post post;

		public Post Post {
			get { return post; }
		}

		protected void Page_Load(object sender, EventArgs e) {
			LoadData();
			this.Title += post.TitleFormatted;
			if (CurrentUser.User != post.Author) // пусть пока будет так TODO: приделать чтобы работало через кукисы
			{
				post.UpdateViews();
			}
			DescSeparator.Text = (post.Description.Trim() == "" || post.Text.Trim() == "") ? "" : "<hr />";
			EditorToolbar.InputId = TextBoxComment.ClientID;
		}

		private void LoadData() {
			LoadPost();
			PostRating.DataBind();
			LoadCommentData();
			LoadComments();
		}
		/// <summary>
		/// Загрузка автора нового коммента и капчи(если нужно).
		/// </summary>
		private void LoadCommentData() {
			if (CurrentUser.isAuth) {
				userLogin.Text = CurrentUser.User.Login;
			}
			else {
				userLogin.Text = "anonymous";
				Captcha.Visible = true;
			}
		}

		private void LoadPost() {
			post = Post.GetById(GetPostId());
			if (post.Id > 0) {
                if (post.IsCurrentUserCanEdit || CurrentUser.IsAdmin)
                {
                    EditPostLink.Text = "/ <a href='editpost.aspx?id=" + post.Id + "' title='Отредактировать новость'>редактировать</a> /";
                    EditPostLink.Visible = true;
                }
                if (post.IsCurrentUserCanDel)
                {
                    DeletePostLink.Visible = true;
                }

				WritePostCategories(post);
				HyperLinkTitle.Text = post.TitleFormatted;
				HyperLinkTitle.NavigateUrl = "news.aspx?id=" + post.Id;

				desc.Text = post.DescriptionFormatted == "" ? "" : "<div class=\"post-desc\">" + post.DescriptionFormatted + "</div>";
				text.Text = post.TextFormatted == "" ? "" : "<div id=\"cut\" class=\"post-text\">" + post.TextFormatted + "</div>";
				comments_count.Text = post.CommentsCount.ToString();
				date.Text = post.CreateDate.ToString("dd MMMM yyyy, HH:mm");
				favorite.Text = post.FavoritesAction;
                bool isBadSource = post.SourceFormatted.Length == 0 && post.Source.Length != 0;
                if (isBadSource)
                {
                    badSource.Text = "<span class=\"bad-source\">Источник: " + HttpUtility.HtmlEncode(post.Source) + "</span>";
                } else
                {
                    if (post.SourceFormatted.Length != 0)
                    {
                        source.Text = "/ <a href=\"" + post.SourceFormatted + "\" target=\"_blank\">источник</a>";
                    }
                }
				// Хреново сделал, дублирование
				authorLogin.Text = author.Text = post.Author.Login;
				views.Text = post.Views.ToString();
			}
			else {
				Response.Redirect(FormsAuthentication.DefaultUrl);
			}
		}
		private void WritePostCategories(Post post) {
			string result = string.Empty;
			foreach (Category cat in Category.GetPostCategories(post.Id)) {
				if (result.Length > 0) {
					result += ", ";
				}
				result += "<a href=\"default.aspx?cat=" + cat.Id + "\" class=\"category-link\">" + cat.Name + "</a>";
			}
			LinksPostCategories.Text = result;
		}
		private int GetPostId() {
			return GetRequestParameter("id");
		}
		private int GetDelCommentId() {
			return GetRequestParameter("cid");
		}

		private int GetRequestParameter(string name) {
			int paramValue = -1;
			Int32.TryParse(Request.QueryString[name], out paramValue);
			return paramValue;
		}
		private void LoadComments() {
			CommentsList.DataBind(Comment.GetByPost(GetPostId()));

		}

		protected void LinkButtonAddComment_Click(object sender, EventArgs e) {
            Comment comm = new Comment();
            comm.Author = CurrentUser.User;
            comm.CreateDate = DateTime.Now;
            comm.Ip = CurrentUser.Ip;
            comm.Post = post;
            comm.Text = TextBoxComment.Text;
            string error = "";
            if (comm.Text.Trim() == "")
            {
                error = "Комментарий не может быть пустым";
            }
            if (error.Length == 0)
            {

                AddCommentErrors.Text = "";
                bool addComment = false;
                if (Captcha.Visible)
                {
                    if (Captcha.IsRightAnswer())
                    {
                        addComment = true;
                    } else
                    {
                        Captcha.SetErrorMessageVisible();
                        LinkButtonAddComment.Focus();
                    }
                } else
                {
                    addComment = true;
                }
                if (addComment)
                {
                    Comment.Add(comm);
                    Response.Redirect("news.aspx?id=" + post.Id + "#comment-" + comm.Id);
                }
            } else
            {
                AddCommentErrors.Text = "<div class=\"error\">" + error + "</div>";
            }
		}
        /*
		protected void AddComment() {
			Comment comm = new Comment();
			comm.Author = CurrentUser.User;
			comm.CreateDate = DateTime.Now;
			comm.Ip = CurrentUser.Ip;
			comm.Post = post;
			comm.Text = TextBoxComment.Text;
            string error = "";
            if (comm.Text.Trim() == "")
            {
                error = "Комментарий не может быть пустым";
            }
            if (comm.Text.Trim().Length > 5048)
            {
                error = "Длина комментария слижком большая";
            }
            if (error.Length == 0)
            {
                Comment.Add(comm);
                Response.Redirect("news.aspx?id=" + post.Id + "#comment-" + comm.Id);
            } else
            {
                AddCommentErrors.Text = "<div class=\"error\">" + error + "</div>";
            }
	
		}
        */
		protected void DeletePost_Click(object sender, EventArgs e) {
            if (post.IsCurrentUserCanDel)
            {
                Post.Delete(post);
            }
			Response.Redirect("default.aspx");
		}
	}
}
