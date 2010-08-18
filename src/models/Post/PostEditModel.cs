using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models {

    public class PostEditModel {

        [Required(ErrorMessage = "Напишите заголовок поста")]
        [DisplayName("Заголовок поста")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Напишите краткое описание")]
        [DisplayName("Краткое описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Напишите текст")]
        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Источник")]
        public string Source { get; set; }

        [DisplayName("Закрепленный пост")]
        public bool IsAttached { get; set; }

        public PostEditModel() {
        }

        public PostEditModel(Post post) {
            Title = post.Title;
            Description = post.Description;
            Text = post.Text;
            Source = post.Source;
            IsAttached = post.IsAttached;
        }

        public Post ToPost() {
            var post = new Post();

            post.Title = Title;
            post.Description = Description;
            post.Text = Text;
            post.Source = Source ?? "";
            post.IsAttached = CurrentUser.IsAdmin && IsAttached;
            post.AuthorId = CurrentUser.User.Id;

            foreach (var isAttached in PostEditCategoriesModel.Current.IsAttached) {
                if (isAttached.Value) {
                    post.PostsCategories.Add(new PostsCategory { CategoryId = isAttached.Key });
                }
            }

            return post;
        }

    }
}
