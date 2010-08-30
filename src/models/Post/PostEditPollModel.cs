using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;
using System;
using ITCommunity.Utils;
using System.Web;


namespace ITCommunity.Models {

    public class PostEditPollModel : PostEditModel {

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

        public PostEditPollModel() :
            base() {
            Title = "empty";
            Description = "empty";
            Text = "empty";
        }

        public PostEditPollModel(Post post) :
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
                    var answerText = HttpUtility.HtmlEncode(answer.Trim());
                    poll.PollAnswers.Add(new PollAnswer { Text = answerText });
                }
            }

            return poll;
        }

        public override Post ToPost() {
            var post = base.ToPost();
            post.Title = Topic;
            return post;
        }
    }
}
