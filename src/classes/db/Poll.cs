

namespace ITCommunity.DB {

    public partial class Poll {

        partial void OnLoaded() {
            var loadAuthor = Author;
            PollAnswers.Load();
        }
    }
}
