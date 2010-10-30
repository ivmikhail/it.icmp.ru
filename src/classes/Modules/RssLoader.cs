using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Linq;
using System.Xml;

using ITCommunity.Core;

namespace ITCommunity.Modules {

    public static class RssLoader {

        public static List<SyndicationItem> Load(string uri) {
            var items = AppCache.Get(uri, () => RssLoader.GetItems(uri));

            return items;
        }

        public static List<SyndicationItem> GetItems(string uri) {
            XmlReader reader = XmlReader.Create(uri);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            return feed.Items.ToList();
        }
    }
}