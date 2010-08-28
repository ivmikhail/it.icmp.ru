using System;

using ITCommunity.Core;
using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

    public partial class Poll {

        public bool IsVoted { get; set; }

        public bool IsActive {
            get {
                return EndDate > DateTime.Now;
            }
        }

        public int PostId { get; set; }

        public DateTime EndDate {
            get {
                var post = Posts.Get(PostId);

                return post.CreateDate.AddDays(ActiveDays.Value);
            }
        }

        partial void OnLoaded() {
            PollAnswers.Load();

            foreach (var answer in PollAnswers) {
                answer.Votes.Load();
            }

            IsVoted = Polls.IsUserVoted(CurrentUser.User.Id);
        }
    }
}
