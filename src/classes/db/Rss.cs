using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Xml;

using ITCommunity.Core;
using System.Net;


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
                // сперва пытаемся найти ссылку по атрибуту rel="alternate"
                foreach (var link in Feed.Links) {
                    if (link.RelationshipType == "alternate") {
                        return link.GetAbsoluteUri().AbsoluteUri;
                    }
                }
                // если не нашли по атрибуту, то тупо выбираем первую ссылку, которая не есть ссылка rss
                foreach (var link in Feed.Links) {
                    if (link.GetAbsoluteUri().AbsoluteUri != Uri) {
                        return link.GetAbsoluteUri().AbsoluteUri;
                    }
                }
                // если нет, то что поделать
                return Uri;
            }
        }

        partial void OnLoaded() {
            Feed = GetFeed(Uri);
        }

        public static SyndicationFeed GetFeed(string uri) {
            try {
                using (var reader = XmlReader.Create(uri)) {
                    var feed = SyndicationFeed.Load(reader);
                    return feed;
                }
            } catch (Exception ex) {
                Logger.Log.Error("Не удалой загрузить rss" + Logger.GetUserInfo(), ex);
            }

            return null;
        }
    }
}