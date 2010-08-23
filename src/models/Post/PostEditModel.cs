using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;


namespace ITCommunity.Models {

    public class PostEditModel {

        [DisplayName("Заголовок поста")]
        [Required(ErrorMessage = "Напишите заголовок поста")]
        public string Title { get; set; }

        [DisplayName("Краткое описание")]
        [Required(ErrorMessage = "Напишите краткое описание")]
        public string Description { get; set; }

        [DisplayName("Текст")]
        [Required(ErrorMessage = "Напишите текст")]
        public string Text { get; set; }

        [DisplayName("Источник")]
        public string Source { get; set; }

        public bool IsAttached { get; set; }

        public PostEditModel() {
        }

        public PostEditModel(Post post) {
            Title = post.Title;
            Description = post.Description;
            Text = post.Text;
            Source = post.Source;
            IsAttached = post.IsAttached;

            foreach (var category in post.Categories) {
                PostEditCategoriesModel.Current.Clear();
                PostEditCategoriesModel.Current.IsAttached[category.Id] = true;
            }
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

            PostEditCategoriesModel.Current.Clear();

            return post;
        }
    }
}
