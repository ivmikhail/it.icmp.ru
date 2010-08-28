using System.Web.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;
using System;
using System.Collections.Generic;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class RssController : BaseController
    {
        public class RssActionResult : ActionResult
        {

            public SyndicationFeed Feed { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.ContentType = "application/rss+xml";

                Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
                using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
                {
                    rssFormatter.WriteTo(writer);
                }
            }
        }

        public class AtomActionResult : ActionResult
        {
            public SyndicationFeed Feed { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.ContentType = "application/atom+xml";
                Atom10FeedFormatter formatter = new Atom10FeedFormatter(Feed);
                using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output))
                {
                    formatter.WriteTo(writer);
                }
            }
        }


        public ActionResult Feed(string format)
        {
            string site_domain = Config.SiteAddress;
            string siteTitle = "Ykt IT Community RSS channel";
            string siteDescription = "RSS лента новостей якутского сообщества ИТ-специалистов - " + site_domain;
            Uri siteUrl = new Uri(site_domain);
            SyndicationFeed feed =  new SyndicationFeed(siteTitle, siteDescription, siteUrl);
            
            var posts = Posts.GetLast(Config.GetInt("RssPostsCount"));
            List<SyndicationItem> items = new List<SyndicationItem>();
            foreach (Post post in posts)
            {
                //item.Author = site_domain + "/user.aspx?login=" + post.Author.Login;
                SyndicationItem item = new SyndicationItem(
                    post.Title == "" ? "null" : post.Title,
                    post.Description == "" ? post.TextFormatted : post.DescriptionFormatted,
                    new Uri(site_domain + "/poll/view/" + post.Id), 
                    post.Id.ToString(),
                    post.CreateDate.ToUniversalTime());
                items.Add(item);
            }
            feed.Items = items;

            if ((format ?? "").ToUpper().Equals("ATOM"))
                return new AtomActionResult() { Feed = feed };
            else
                return new RssActionResult() { Feed = feed };

        }

    }
}
