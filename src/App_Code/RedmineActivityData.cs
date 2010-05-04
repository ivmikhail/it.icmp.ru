using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace ITCommunity {

	public static class RedmineActivityData {

		#region For caching

		public const string REDMINE_ACTIVITY_CACHE_KEY = "RedmineActivity";

		//делегат метода загрузки данных
		private delegate object RedmineActivityLoader(int howView);

		private static RedmineActivityLoader _redmineActivityLoader = new RedmineActivityLoader(LoadDataFromRss);

		#endregion

		public static List<RedmineActivityItem> GetItems() {
			var list = AppCache.Get(
				 REDMINE_ACTIVITY_CACHE_KEY,
				_redmineActivityLoader,
				new object[] { Config.GetInt("RedmineActivityRssHowView") }
			);

			return (List<RedmineActivityItem>)list;
		}

		private static List<RedmineActivityItem> LoadDataFromRss(int howGet) {
			List<RedmineActivityItem> result = new List<RedmineActivityItem>();
			string rssUrl = Config.Get("RedmineActivityRssUrl");
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
							}
							else if (reader.Name == "title") {
								isTextTitle = true;
							}
							else if (reader.Name == "author") {
								isTextAuthor = true;
							}
							else if (reader.Name == "content") {
								isTextContent = true;
							}
							else if (reader.Name == "id") {
								isTextUrl = true;
							}
							break;
						case XmlNodeType.Text:
							if (item != null) {
								if (isTextTitle) {
									item.Title = Util.HtmlEncode(reader.Value);
									isTextTitle = false;
								}
								else if (isTextAuthor) {
									item.Author = Util.HtmlEncode(reader.Value);
									isTextAuthor = false;
								}
								else if (isTextContent) {
									item.Text = Util.HtmlEncode(reader.Value);
									isTextContent = false;
								}
								else if (isTextUrl) {
									item.Url = Util.HtmlEncode(reader.Value);
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
			}
			catch (WebException ex) {
				Logger.Log.Error("Не удалось получить активность redmine, rss feed url - " + rssUrl, ex);
			}
			finally {
				if (response != null) {
					response.Close();
				}
			}
			return result;
		}
	}
}
