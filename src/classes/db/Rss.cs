using System.ServiceModel.Syndication;
using System.Xml;


namespace ITCommunity.DB {

    public partial class Rss {

        public SyndicationFeed Feed { get; private set; }

        partial void OnLoaded() {
            Feed = GetFeed(Uri);
        }

        public static SyndicationFeed GetFeed(string uri) {
            XmlReader reader = XmlReader.Create(uri);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            return feed;
        }
    }
}