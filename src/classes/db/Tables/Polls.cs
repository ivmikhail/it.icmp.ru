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

                poll.Topic = editedPoll.Topic;
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

                if (poll != null) {
                    poll.Votes.Load();
                }

                return poll;
            }
        }

        public static void DeleteAnswer(int id) {
            using (var db = Database.Connect()) {
                var answer = (
                    from ans in db.PollAnswers
                    where ans.Id == id
                    select ans
                ).SingleOrDefault();

                db.PollAnswers.DeleteOnSubmit(answer);
                db.SubmitChanges();
            }
        }
    }
}
