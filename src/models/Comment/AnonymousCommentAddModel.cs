﻿using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;


namespace ITCommunity.Models {

    public class AnonymousCommentAddModel : CaptchaModel {

        public int PostId { get; set; }

        [Required(ErrorMessage = "Введите текст комментария")]
        public string Text { get; set; }

        public Comment ToComment() {
            var comment = new Comment();

            comment.PostId = PostId;
            comment.Text = Text;
            comment.AuthorId = CurrentUser.User.Id;
            comment.Ip = CurrentUser.Ip;

            return comment;
        }
    }
}