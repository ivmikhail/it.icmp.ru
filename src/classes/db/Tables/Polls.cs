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

                if (poll != null) {
                    db.Polls.DeleteOnSubmit(poll);
                    db.SubmitChanges();
                }
            }
        }

        public static void Update(Poll editedPoll) {
            using (var db = Database.Connect()) {
                var poll = (
                    from pll in db.Polls
                    where pll.Id == editedPoll.Id
                    select pll
                ).Single();

                poll.ActiveDays = editedPoll.ActiveDays;

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
                // нужно проверить, не голосовал ли юзер
                // актуально для опроса с несколько выбираемыми ответами
                var isVoted = (
                    from vot in db.Votes
                    from ans in db.PollAnswers
                    where
                        vot.UserId == vote.UserId &&
                        ans.Id == vote.AnswerId &&
                        vot.PollAnswer.PollId == ans.PollId
                    select vot
                ).Any();

                // этот ответ не будет учитываться
                db.Votes.InsertOnSubmit(vote);
                db.SubmitChanges();

                if (isVoted == false) {
                    vote.PollAnswer.Poll.VotedUsersCount++;
                    db.SubmitChanges();
                }

                return vote;
            }
        }

        public static bool IsUserVoted(int id, int userId) {
            using (var db = Database.Connect()) {
                var vote =
                    from ans in db.PollAnswers
                    from vot in db.Votes
                    where 
                        vot.UserId == userId &&
                        ans.PollId == id &&
                        vot.AnswerId == ans.Id
                    select vot;

                return vote.Any();
            }
        }
    }
}
