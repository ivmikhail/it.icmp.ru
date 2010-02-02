using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using Rss;

namespace ITCommunity
{

    public partial class RssFeedPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RssFeed feed = new RssFeed();
            feed.Channels.Add(GetChannel());
            Response.ContentType = "text/xml";
            feed.Encoding = Encoding.UTF8;
            feed.Write(Response.OutputStream);
            Response.End();
        }
        private RssChannel GetChannel()
        {
            RssChannel channel = new RssChannel();

            string site_domain = Global.SiteAddress;
            List<Post> last_posts = Post.GetLast(Global.ConfigNumParam("RssPostsCount"));
            foreach (Post post in last_posts)
            {
                RssItem item = new RssItem();

                item.Author = site_domain + "/user.aspx?login=" + post.Author.Login;
                item.Link = new Uri(site_domain + "/news.aspx?id=" + post.Id);
                item.Title = post.Title == "" ? "null" : post.Title;
                item.Description = post.Description == "" ? post.TextFormatted : post.DescriptionFormatted;
                item.PubDate = post.CreateDate.ToUniversalTime();

                channel.Items.Add(item);
            }

            channel.Title = "Ykt IT Community RSS channel";
            channel.Description = "RSS лента новостей якутского сообщества ИТ-специалистов - " + site_domain;
            channel.Link = new Uri(site_domain);
            channel.LastBuildDate = channel.Items.LatestPubDate();
            channel.Copyright = "Ykt IT Community (c) 2007 - " + DateTime.Now.Year;
            channel.Language = "ru";
            channel.Generator = "RSS.NET - .NET class library for RSS/feeds";
            channel.Docs = "http://backend.userland.com/rss";

            return channel;
        }
    }
}
