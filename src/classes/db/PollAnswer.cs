using System.Web;


namespace ITCommunity.DB {

    public partial class PollAnswer {

        public string TextFormatted {
            get { return HttpUtility.HtmlEncode(Text); }
        }

        partial void OnLoaded() {
            Votes.Load();
        }
    }
}
