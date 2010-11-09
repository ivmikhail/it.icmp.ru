using System.Web;


namespace ITCommunity.DB {

    public partial class PollAnswer {

        public string TextFormatted {
            get { return HttpUtility.HtmlEncode(Text); }
        }

        public double Percent {
            get {
                double count = Votes.Count;
                return 100 * count / Poll.TotalVotesCount;
            }
        }

        partial void OnLoaded() {
            Votes.Load();
        }
    }
}
