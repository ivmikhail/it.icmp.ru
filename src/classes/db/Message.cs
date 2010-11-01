using System.Web;

using ITCommunity.Utils;


namespace ITCommunity.DB {

    public partial class Message {

        public string TitleFormatted {
            get { return BBCodeParser.Format(HttpUtility.HtmlEncode(Title)); }
        }

        public string TextFormatted {
            get { return BBCodeParser.Format(HttpUtility.HtmlEncode(Text)); }
        }

        partial void OnLoaded() {
            // см Post.OnLoaded()
            var loadReceiver = Receiver;
            var loadSender = Sender;
        }
    }
}
