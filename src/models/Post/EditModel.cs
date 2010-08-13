using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.Db.Tables;


namespace ITCommunity.Models.Post {

    public class EditModel {

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

        public EditModel() {
        }

        public EditModel(Db.Post post) {
            Title = post.Title;
            Description = post.Description;
            Text = post.Text;
            Source = post.Source;
        }

        public Db.Post ToPost() {
            var post = new Db.Post();

            post.Title = Title;
            post.Description = Description;
            post.Text = Text;
            post.Source = Source ?? "";
            post.AuthorId = CurrentUser.User.Id;

            post.Categories = new List<Db.Category>();

            foreach (var isAttached in EditCategoriesModel.Current.IsAttached) {
                if (isAttached.Value) {
                    var category = Categories.Get(isAttached.Key);
                    post.Categories.Add(category);
                }
            }

            return post;
        }

    }
}
