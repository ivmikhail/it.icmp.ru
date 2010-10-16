using System.Web.Mvc;

using ITCommunity.Models;

namespace ITCommunity.Controllers {

    public class PostController : BasePostController {

        [Authorize]
        public ActionResult Add() {
            return base.BaseAdd();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PostEditModel model) {
            return base.BaseAdd(model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            return base.BaseEdit(id);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PostEditModel model) {
            return base.BaseEdit(id, model);
        }

        public ActionResult List(int? page) {
            var model = new PostListModel(PostListModel.SortBy.Date, page);

            return View("ListPage", model);
        }

        public ActionResult PopularList(string period, int? page) {
            var model = new PostListModel(PostListModel.SortBy.Views, period, page);

            return View(model);
        }

        public ActionResult DiscussibleList(string period, int? page) {
            var model = new PostListModel(PostListModel.SortBy.Comments, period, page);

            return View(model);
        }

        public ActionResult Search(string query) {
            return View();
        }

    }
}
