using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;

namespace ITCommunity {
    public class RedmineActivityData {
        //делегат метода загрузки данных
        private delegate object RedmineActivityLoader(int howView);
        private static readonly int cacheLiveTime = Global.ConfigNumParam("RedmineActivityCachePer");
        private static readonly int howItemsGet = Global.ConfigNumParam("RedmineActivityRssHowView");
        private static readonly String cacheKeyName = Global.ConfigStringParam("RedmineActivityCacheName");
 
        public static List<RedmineActivityItem> GetItems() {
            RedmineActivityLoader loader = new RedmineActivityLoader(LoadDataFromRss);
            List<RedmineActivityItem> list = (List<RedmineActivityItem>)AppCache.Get(cacheKeyName, new object(), loader, new object[] { howItemsGet }, DateTime.Now.AddMinutes(cacheLiveTime));
            return list;
        }
        private static List<RedmineActivityItem> LoadDataFromRss(int howGet) {
            List<RedmineActivityItem> result = new List<RedmineActivityItem>();
            String rssUrl = Global.ConfigStringParam("RedmineActivityRssUrl");
            // не пашет
            // AtomFeed feed = AtomFeed.Load(new Uri(rssUrl)); -

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(rssUrl);
            HttpWebResponse response = null;
            try {
                response = (HttpWebResponse)request.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlTextReader reader = new XmlTextReader(stream);

                bool isTextTitle = false;
                bool isTextAuthor = false;
                bool isTextContent = false;
                bool isTextUrl = false;
                RedmineActivityItem item = null;

                while (reader.Read() && result.Count < howGet) {

                    switch (reader.NodeType) {
                        case XmlNodeType.Element:
                            if (reader.Name == "entry") {
                                item = new RedmineActivityItem();
                                isTextTitle = false;
                                isTextAuthor = false;
                                isTextContent = false;
                                isTextUrl = false;
                            } else if (reader.Name == "title") {
                                isTextTitle = true;
                            } else if (reader.Name == "author") {
                                isTextAuthor = true;
                            } else if (reader.Name == "content") {
                                isTextContent = true;
                            } else if (reader.Name == "id") {
                                isTextUrl = true;
                            }
                            break;
                        case XmlNodeType.Text:
                            if (item != null) {
                                if (isTextTitle) {
                                    item.Title = HttpUtility.HtmlEncode(reader.Value);
                                    isTextTitle = false;
                                } else if (isTextAuthor) {
                                    item.Author = HttpUtility.HtmlEncode(reader.Value);
                                    isTextAuthor = false;
                                } else if (isTextContent) {
                                    item.Text = HttpUtility.HtmlEncode(reader.Value);
                                    isTextContent = false;
                                } else if (isTextUrl) {
                                    item.Url = HttpUtility.HtmlEncode(reader.Value);
                                    isTextUrl = false;
                                }
                            }
                            break;
                        case XmlNodeType.EndElement:
                            if (reader.Name == "entry") {
                                result.Add(item);
                            }
                            break;
                    }
                }
                reader.Close();
            } catch (WebException ex) {
                Logger.Log.Error("Не удалось получить активность redmine, rss fedd url - " + rssUrl, ex);
            } finally {
                if (response != null) {
                    response.Close();
                }
            }
            return result;
        }
    }
}
