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
	public partial class CommentsList : System.Web.UI.UserControl {

        private const string EDITABLE_COMMENT_INDX_NAME      = "editable_comment_index";
        private const string EDIT_COMMENT_MESSAGE            = "edit_message";
		protected void Page_Load(object sender, EventArgs e) {
        }
        
		public void DataBind(List<Comment> comments) {
			RepeaterComments.DataSource = comments;
			RepeaterComments.DataBind();
			Visible = (comments.Count > 0);
		}

		protected void RepeaterComments_ItemCommand(object source, RepeaterCommandEventArgs e) {

            string[] args = e.CommandArgument.ToString().Split(',');
            int commentId = Convert.ToInt32(args[0]);
            int postId    = Convert.ToInt32(args[1]);

            Comment editableComment = Comment.GetById(commentId);
            bool isCanEdit = editableComment.IsCurrentUserCanEdit;
            bool isCanDel  = editableComment.IsCurrentUserCanDel;
            if (e.CommandName == "delete")
            {
                if (isCanDel)
                {
                     Comment.Delete(commentId);
                } else
                {
                    Session[EDIT_COMMENT_MESSAGE] = "<div class=\"error\">редактирование невозможно, время редактирования истекло</div>";
                }
            } else if (e.CommandName == "update")
            {
                string newText = NewTextHidden.Value;
                if (isCanEdit && newText != "")
                {
                    editableComment.Text = newText;
                    editableComment.Update();
                    Session[EDITABLE_COMMENT_INDX_NAME] = null;
                } else
                {
                    Session[EDIT_COMMENT_MESSAGE] = newText == "" ? "<div class=\"error\">редактирование невозможно, текст не может быть пустым</div>" : "<div class=\"error\">редактирование невозможно, время редактирования истекло</div>";
                }

            } else if (e.CommandName == "edit")
            {
                if (isCanEdit)
                {
                    Session[EDITABLE_COMMENT_INDX_NAME] = e.Item.ItemIndex;
                } else
                {
                   Session[EDIT_COMMENT_MESSAGE] = "<div class=\"error\">редактирование невозможно, время редактирования истекло</div>";
                }
            } else if (e.CommandName == "cancel")
            {
                Session[EDITABLE_COMMENT_INDX_NAME]     = null;
                Session[EDIT_COMMENT_MESSAGE]           = null;
            }

            Response.Redirect("news.aspx?id=" + postId + "#comment-" + commentId);
		}

        protected void RepeaterComments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            int editableItemIndx = -1;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Comment currentComment = (Comment)e.Item.DataItem;                
                bool isUserCanEdit = currentComment.IsCurrentUserCanEdit;
                bool isUserCanDel  = currentComment.IsCurrentUserCanDel;

                //Управляющие ссылки
                Literal commentText   = ((Literal)e.Item.FindControl("CommentText"));
                LinkButton editLink   = ((LinkButton)e.Item.FindControl("EditComment"));
                LinkButton deleteLink = ((LinkButton)e.Item.FindControl("DeleteComment"));
                LinkButton updateLink = ((LinkButton)e.Item.FindControl("UpdateComment"));
                LinkButton cancelLink = ((LinkButton)e.Item.FindControl("CancelEdit"));
                Literal    editError  = ((Literal)e.Item.FindControl("EditError"));
                
                // Биндим текст
                commentText.Text = currentComment.TextFormatted;

                // Показать ссылку на удаление
                if (isUserCanDel)
                {
                    deleteLink.Visible = true;
                }
                       
                // Показать ссылку редактировать
                if (isUserCanEdit)
                {
                    editLink.Visible = true;
                }

                if (Session[EDITABLE_COMMENT_INDX_NAME] != null)
                {
                    editableItemIndx = (int)Session[EDITABLE_COMMENT_INDX_NAME];
                }

                //Показать текстбокс для редактирования
                bool isCurrentItemEditable   = editableItemIndx != -1 && editableItemIndx == e.Item.ItemIndex;
                if (isCurrentItemEditable && isUserCanEdit)              
                {

                    TextBox newComment = ((TextBox)e.Item.FindControl("NewCommentText"));
                    newComment.Text = currentComment.Text;

                    newComment.Visible = true;

                    commentText.Visible = false;

                    updateLink.Visible = true;
                    cancelLink.Visible = true;

                    deleteLink.Visible = false;
                    editLink.Visible = false;
                }

                if (isCurrentItemEditable && Session[EDIT_COMMENT_MESSAGE] != null)
                {
                    editError.Text = Session[EDIT_COMMENT_MESSAGE].ToString();
                }
            }
        }
       private int GetParam(string name)
       {
            int id;
            Int32.TryParse(Request.QueryString[name], out id);
            return id;
        }
	}
}
