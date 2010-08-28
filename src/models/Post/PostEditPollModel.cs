using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB;


namespace ITCommunity.Models {

    public class PostEditPollModel : PostEditModel {

        [DisplayName("Вопрос")]
        [Required(ErrorMessage = "Напишите вопрос")]
        public string Topic { get; set; }

        [DisplayName("Варианты ответов")]
        [Required(ErrorMessage = "Напишите варианты ответов")]
        public string Answers { get; set; }

        [DisplayName("Сколько дней активен опрос")]
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
                poll.PollAnswers.Add(new PollAnswer { Text = answer });
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
