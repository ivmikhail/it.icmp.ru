using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCommunity.Models;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using ITCommunity.Core;

namespace ITCommunity.Controllers {

    public class PostsController : BaseController {

        public ActionResult Index(int? page) {
            var model = new PostsModel(PostsModel.SortBy.Date, page);

            return View(model);
        }

        public ActionResult Popular(string period, int? page) {
            var model = new PostsModel(PostsModel.SortBy.Views, period, page);

            return View(model);
        }

        public ActionResult Discussible(string period, int? page) {
            var model = new PostsModel(PostsModel.SortBy.Comments, period, page);

            return View(model);
        }

        public ActionResult Search(string query) {
            return View();
        }

    }
}
