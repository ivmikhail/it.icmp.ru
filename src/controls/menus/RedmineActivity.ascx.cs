using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Diagnostics;
using System.IO;
using Rss;
using System.Xml;
namespace ITCommunity {
    public partial class RedmineActivity : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {
            int howItemsGet = Global.ConfigNumParam("RedmineActivityRssHowView");
            List<RedmineActivityItem> items = loadDataFromRss(howItemsGet);
            ActivityItems.DataSource = items;
            ActivityItems.DataBind();
        }

        private List<RedmineActivityItem> loadDataFromRss(int howGet) {
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
                                        item.Title = reader.Value;
                                        isTextTitle = false;
                                    } else if (isTextAuthor) {
                                        item.Author = reader.Value;
                                        isTextAuthor = false;
                                    } else if (isTextContent) {
                                        item.Text = reader.Value;
                                        isTextContent = false;
                                    } else if (isTextUrl) {
                                        item.Url = reader.Value;
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
                    Debug.Print(ex.Message);
                } finally {
                    if (response != null) {
                        response.Close();
                    }
                }
            return result;
        }
    }
}