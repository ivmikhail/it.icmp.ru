using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;


namespace ITCommunity.Models {

    public class AnonymousCommentAddModel : CaptchaModel {

        public int PostId { get; set; }

        [Required(ErrorMessage = "Введите текст комментария")]
        public string Text { get; set; }

        public Db.Comment ToComment() {
            var comment = new Db.Comment();

            comment.PostId = PostId;
            comment.Text = Text;
            comment.AuthorId = CurrentUser.User.Id;
            comment.Ip = CurrentUser.Ip;

            return comment;
        }
    }
}
