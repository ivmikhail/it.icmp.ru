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

            int catId = 0;
            int count = 0;
            IList<Post> posts = null;
            if (catId > 0)
            {
                posts = Post.GetByCategory(1, 20, catId, ref count);
            } else
            {
                posts = Post.Get(1, 20, ref count);
            }
            ViewData["posts"] = posts;

            return View("list");
        }
        public ActionResult View(int id)
        {
            Post post = Post.GetById(id);
            ViewData["comments"] = Comment.GetByPost(post.Id);
            ViewData["post"] = post;
            return View("view");
        }
        public ActionResult Category()
        {
            return View("list");
        }
    }
}
