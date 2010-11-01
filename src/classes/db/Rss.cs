using System;
using System.ServiceModel.Syndication;
using System.Xml;
using ITCommunity.Core;


namespace ITCommunity.DB {

    public partial class Rss {

        public SyndicationFeed Feed { get; private set; }

        public string Description {
            get {
                if (Feed == null) {
                    return "Rss-лента не загрузилась";
                }
                if (Feed.Description != null) {
                    return Feed.Description.Text;
                }
                if (Feed.Title != null) {
                    return Feed.Title.Text;
                }
                return Title;
            }
        }

        public string AlternateUrl {
            get {
                if (Feed == null) {
                    return Uri;
                }
                foreach (var link in Feed.Links) {
                    if (link.RelationshipType == "alternate") {
                        return link.GetAbsoluteUri().AbsoluteUri;
                    }
                }
                return Uri;
            }
        }

        partial void OnLoaded() {
            Feed = GetFeed(Uri);
        }

        public static SyndicationFeed GetFeed(string uri) {
            try {
                XmlReader reader = XmlReader.Create(uri);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                return feed;
            } catch (Exception ex) {
                Logger.Log.Error("Не удалой загрузить rss" + Logger.GetUserInfo(), ex);
            }

            return null;
        }
    }
}