using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


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
            var siteDescription = "RSS лента новостей якутского сообщества ИТ-специалистов";
            var siteUrl = new Uri(Config.SiteAddress);
            var feed = new SyndicationFeed(siteTitle, siteDescription, siteUrl);

            var posts = Posts.GetLast(Config.RssPostsCount);
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

                var authorUrl = Config.SiteAddress + Url.Action("profile", "user", new { nick = post.Author.Nick });
                var itemAuthor = new SyndicationPerson("", post.Author.Nick, authorUrl);
                item.Authors.Add(itemAuthor);

                foreach (var category in post.Categories) {
                    var itemCategory = new SyndicationCategory(category.Name);
                    item.Categories.Add(itemCategory);
                }

                items.Add(item);
            }

            feed.Items = items;

            return new RssActionResult() { Feed = feed };
        }

        public ActionResult Load(int? id) {
            if (id == 0) {
                if (Rsses.All.Count == 0) {
                    return NotFound();
                }
                return View(Rsses.All[0]);
            }

            var rss = Rsses.Get(id.Value);

            return View(rss);
        }

        [Authorize(Roles = "admin")]
        public ActionResult List(int? id) {
            if (id == 0) {
                return View();
            }

            var model = new RssEditModel(id.Value);

            return View("ListPage", model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult List(int? id, RssEditModel model) {
            if (ModelState.IsValid) {
                var rss = model.ToRss();

                if (id == 0) {
                    Rsses.Add(rss);
                } else {
                    rss.Id = id.Value;
                    Rsses.Update(rss);
                }

                return RedirectToAction("list", new { id = 0 });
            }

            return View("ListPage", model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Rsses.Delete(id.Value);

            return RedirectToAction("list");
        }
    }
}
