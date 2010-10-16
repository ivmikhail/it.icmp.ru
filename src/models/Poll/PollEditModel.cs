using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;


namespace ITCommunity.Models {

    public class PollEditModel : PostEditModel {

        [DisplayName("Вопрос")]
        [Required(ErrorMessage = "Напишите вопрос")]
        public string Topic { get; set; }

        [DisplayName("Варианты ответов")]
        [Required(ErrorMessage = "Напишите варианты ответов", AllowEmptyStrings = false)]
        public string Answers { get; set; }

        [DisplayName("Сколько дней активен опрос")]
        [Range(1, 365, ErrorMessage = "Значение дней может быть только от 1 до 365")]
        public int? ActiveDays { get; set; }

        public bool IsMultiselect { get; set; }

        public bool IsOpen { get; set; }

        public PollEditModel() :
            base() {
            Title = "empty";
            Description = "empty";
            Editors = new Dictionary<string, string>();
            Editors.Add("Text", "текст");
        }

        public PollEditModel(Post post) :
            base(post) {
            var poll = (Poll)post.Entity;

            Topic = post.Title;
            IsMultiselect = poll.IsMultiselect;
            IsOpen = poll.IsOpen;
            ActiveDays = poll.ActiveDays;

            Answers = "";
            foreach (var answer in poll.PollAnswers) {
                Answers += answer.Text + "\n";
            }
        }

        public Poll ToPoll() {
            var poll = new Poll();

            poll.IsMultiselect = IsMultiselect;
            poll.IsOpen = IsOpen;
            poll.ActiveDays = ActiveDays;

            var answers = Answers.Split('\n');
            foreach (var answer in answers) {
                if (String.IsNullOrWhiteSpace(answer) == false) {
                    poll.PollAnswers.Add(new PollAnswer { Text = answer.Trim() });
                }
            }

            return poll;
        }

        public override Post ToPost() {
            if (Poll.Category != null) {
                PostEditCategoriesModel.Current.IsAttached[Poll.Category.Id] = true;
            }

            var post = base.ToPost();
            post.Title = Topic;
            post.Description = "";

            return post;
        }
    }
}
