using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCommunity.Models;
using ITCommunity.Db;

namespace ITCommunity.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

        public ActionResult Index()
        {

            int count = 0;
            IList<Post> posts = Post.GetTopByViews(365, 10);
            ViewData["posts"] = posts;

            return View("list");
        }
        public ActionResult Category()
        {
            return View("list");
        }
    }
}
