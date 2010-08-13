using System.ComponentModel.DataAnnotations;

using ITCommunity.Models.Captcha;


namespace ITCommunity.Models.Comment {

    public class AnonymousAddModel : CaptchaModel {

        public int PostId { get; set; }

        [Required(ErrorMessage = "Введите текст комментария")]
        public string Text { get; set; }

        public AnonymousAddModel() {
        }

        public AnonymousAddModel(int postId) {
            PostId = postId;
        }
    }
}
