using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;


namespace ITCommunity.Models {

    public class PostEditModel : PictureUploadModel {

        [DisplayName("Заголовок поста")]
        [Required(ErrorMessage = "Напишите заголовок поста")]
        public string Title { get; set; }

        [DisplayName("Краткое описание")]
        [Required(ErrorMessage = "Напишите краткое описание")]
        public string Description { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Источник")]
        public string Source { get; set; }

        public bool IsAttached { get; set; }

        public bool IsCommentable { get; set; }

        public Post.EntityTypes? EntityType { get; set; }

        public int? EntityId { get; set; }

        [Required(ErrorMessage = "Выберите категории")]
        public bool? IsSetCategory {
            get {
                if (IsSetCategories()) {
                    return true;
                } else {
                    return null;
                }
            }
        }

        public PostEditModel() {
            Path = Post.DefaultPicturesPath;
            IsCommentable = true;
            PictureTextareas = new Dictionary<string, string>();
            PictureTextareas.Add("Description", "описание");
            PictureTextareas.Add("Text", "текст");
        }

        public PostEditModel(Post post) :
            this() {
            Title = post.Title;
            Description = post.Description;
            Text = post.Text;
            Source = post.Source;
            IsAttached = post.IsAttached;
            IsCommentable = post.IsCommentable;
            EntityType = post.EntityType;
            EntityId = post.EntityId;
            Path = post.PicturesPath;

            PostEditCategoriesModel.Current.Clear();
            foreach (var category in post.Categories) {
                PostEditCategoriesModel.Current.IsAttached[category.Id] = true;
            }
        }

        protected virtual bool IsSetCategories() {
            return PostEditCategoriesModel.Current.AttachedCount > 0;
        }


        public virtual Post ToPost() {
            var post = new Post();

            post.Title = Title;
            post.Description = Description;
            post.Text = Text ?? "";
            post.Source = Source ?? "";
            post.IsAttached = CurrentUser.IsAdmin && IsAttached;
            post.IsCommentable = IsCommentable;
            post.AuthorId = CurrentUser.User.Id;
            post.EntityType = EntityType ?? Post.EntityTypes.Post;
            post.EntityId = EntityId;

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
