using System.Linq;


namespace ITCommunity.DB.Tables {

    public static class Polls {

        public static Poll Add(Poll poll) {
            using (var db = Database.Connect()) {
                db.Polls.InsertOnSubmit(poll);
                db.SubmitChanges();

                return poll;
            }
        }

        public static void Delete(int id) {
            using (var db = Database.Connect()) {
                var poll = (
                    from pll in db.Polls
                    where pll.Id == id
                    select pll
                ).SingleOrDefault();

                db.Polls.DeleteOnSubmit(poll);
                db.SubmitChanges();
            }
        }

        public static void Update(Poll editedPoll) {
            using (var db = Database.Connect()) {
                var poll = (
                    from pll in db.Polls
                    where pll.Id == editedPoll.Id
                    select pll
                ).SingleOrDefault();

                poll.IsMultiselect = editedPoll.IsMultiselect;
                poll.IsOpen = editedPoll.IsOpen;
                poll.PollAnswers = editedPoll.PollAnswers;

                db.SubmitChanges();
            }
        }

        public static Poll Get(int id) {
            using (var db = Database.Connect()) {
                var poll = (
                    from pll in db.Polls
                    where pll.Id == id
                    select pll
                ).SingleOrDefault();

                return poll;
            }
        }

        public static Vote AddVote(Vote vote) {
            using (var db = Database.Connect()) {
                db.Votes.InsertOnSubmit(vote);
                db.SubmitChanges();

                vote.PollAnswer.Poll.VotesCount++;
                db.SubmitChanges();

                return vote;
            }
        }

        public static bool IsUserVoted(int userId) {
            using (var db = Database.Connect()) {
                var vote =
                    from vot in db.Votes
                    where vot.UserId == userId
                    select vot;

                return vote.Any();
            }
        }
    }
}
