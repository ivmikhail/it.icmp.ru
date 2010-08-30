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
                if (ActiveDays == null) {
                    return DateTime.MaxValue;
                }

                var post = Posts.Get(PostId);
                return post.CreateDate.AddDays(ActiveDays.Value);
            }
        }

        partial void OnLoaded() {
            PollAnswers.Load();

            foreach (var answer in PollAnswers) {
                answer.Votes.Load();
            }

            IsVoted = Polls.IsUserVoted(Id, CurrentUser.User.Id);
        }
    }
}
