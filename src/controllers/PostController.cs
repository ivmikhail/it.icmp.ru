using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class PostController : BaseController {

        public ActionResult View(int? id) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (post != null) {
                if (CurrentUser.User.Id != post.AuthorId) {
                    Posts.IncViews(post);
                }
                return View(post);
            }

            return NotFound();
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

        [Authorize]
        public ActionResult Add() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PostEditModel model) {
            if (ModelState.IsValid) {
                var post = model.ToPost();

                post = Posts.Add(post);

                if (CurrentUser.IsAdmin == false) {
                    var user = CurrentUser.User;
                    user.HeadersCounter++;
                    Users.Update(user);
                }

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (CurrentUser.IsAdmin == false && post.AuthorId != CurrentUser.User.Id) {
                return AccessDenied();
            }

            var model = new PostEditModel(post);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PostEditModel model) {
            if (id == 0) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (CurrentUser.IsAdmin == false && post.AuthorId != CurrentUser.User.Id) {
                return AccessDenied();
            }

            if (ModelState.IsValid) {
                var editedPost = model.ToPost();
                editedPost.Id = post.Id;

                Posts.Update(editedPost);

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ToggleCategory(int? categoryId) {
            if (!Request.IsAjaxRequest()) {
                return AccessDenied();
            }

            if (categoryId == null) {
                return NotFound();
            }

            PostEditCategoriesModel.Current.IsAttached[categoryId.Value] = !PostEditCategoriesModel.Current.IsAttached[categoryId.Value];

            return PartialView("EditCategories", PostEditCategoriesModel.Current);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Posts.Delete(id.Value);

            return Redirect("/");
        }
    }
}
