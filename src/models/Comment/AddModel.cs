using System.ComponentModel.DataAnnotations;

using ITCommunity.Models.Captcha;


namespace ITCommunity.Models.Comment {

    public class AddModel {

        public int PostId { get; set; }

        [Required(ErrorMessage = "Введите текст комментария")]
        public string Text { get; set; }

        public AddModel() {
        }

        public AddModel(int postId) {
            PostId = postId;
        }
    }
}
