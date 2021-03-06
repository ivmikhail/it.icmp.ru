using System;

using ITCommunity.Core;
using ITCommunity.DB.Tables;


namespace ITCommunity.DB {

    public partial class Poll {

        public static Category Category {
            get {
                int categoryId = Config.PollCategoryId;
                return Categories.Get(categoryId);
            }
        }

        public bool IsVoted { get; set; }

        public bool IsActive {
            get { return EndDate > DateTime.Now; }
        }

        public DateTime EndDate {
            get {
                if (ActiveDays == null) {
                    return DateTime.MaxValue;
                }

                var post = Posts.GetByEntity(Id);
                return post.CreateDate.AddDays(ActiveDays.Value);
            }
        }

        public Post Post {
            get { return Posts.GetByEntity(Id); }
        }

        public int TotalVotesCount {
            get {
                int count = 0;
                foreach (var answer in PollAnswers) {
                    count += answer.Votes.Count;
                }
                return count;
            }
        }

        public bool ContainsAnswer(int answerId) {
            foreach (var answer in PollAnswers) {
                if (answer.Id == answerId) {
                    return true;
                }
            }
            return false;
        }

        partial void OnLoaded() {
            PollAnswers.Load();

            IsVoted = Polls.IsUserVoted(Id, CurrentUser.User.Id);
        }
    }
}
