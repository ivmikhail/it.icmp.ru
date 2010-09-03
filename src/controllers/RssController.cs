using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;


namespace ITCommunity.Controllers {

    public class RssController : BaseController {

        public class RssActionResult : ActionResult {

            public SyndicationFeed Feed { get; set; }

            public override void ExecuteResult(ControllerContext context) {
                context.HttpContext.Response.ContentType = "application/rss+xml";

                Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(Feed);
                using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output)) {
                    rssFormatter.WriteTo(writer);
                }
            }
        }

        public ActionResult Feed() {
            var siteTitle = "Ykt IT Community RSS channel";
            var siteDescription = "RSS лента новостей якутского сообщества ИТ-специалистов - " + Config.SiteAddress;
            var siteUrl = new Uri(Config.SiteAddress);
            var feed = new SyndicationFeed(siteTitle, siteDescription, siteUrl);

            var posts = Posts.GetLast(Config.GetInt("RssPostsCount"));
            var items = new List<SyndicationItem>();
            foreach (Post post in posts) {
                var url = Config.SiteAddress + Url.Action("view", "post", new { id = post.Id });

                var item = new SyndicationItem(
                    post.Title == "" ? "null" : post.Title,
                    post.Description == "" ? post.TextFormatted : post.DescriptionFormatted,
                    new Uri(url),
                    post.Id.ToString(),
                    post.CreateDate.ToUniversalTime()
                );
                items.Add(item);
            }

            feed.Items = items;

            return new RssActionResult() { Feed = feed };
        }
    }
}
