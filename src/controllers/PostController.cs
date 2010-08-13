using System.Web.Mvc;
using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using ITCommunity.Models;
using ITCommunity.Models.Comment;
using System.Collections.Generic;
using ITCommunity.Models.Post;

namespace ITCommunity.Controllers {

    public class PostController : BaseController {

        public ActionResult View(int? id) {
            if (id == null) {
                return NotFound();
            }

            var model = Posts.Get(id.Value, true);
            if (model != null) {
                return View(model);
            }

            return NotFound();
        }

        [Authorize]
        public ActionResult Add() {
            return View("Edit");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(EditModel model) {
            if (ModelState.IsValid) {
                var post = model.ToPost();

                post = Posts.Add(post);
                Session.Remove(EditCategoriesModel.SESSION_NAME);

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View("Edit", model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (post.AuthorId != CurrentUser.User.Id) {
                return AccessDenied();
            }

            foreach (var category in post.Categories) {
                EditCategoriesModel.Current.IsAttached[category.Id] = true;
            }

            var model = new EditModel(post);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, EditModel model) {
            if (id == null) {
                return NotFound();
            }

            var post = Posts.Get(id.Value);

            if (post.AuthorId != CurrentUser.User.Id) {
                return AccessDenied();
            }

            if (ModelState.IsValid) {
                var editedPost = model.ToPost();
                editedPost.Id = post.Id;

                Posts.Update(editedPost);
                Session.Remove(EditCategoriesModel.SESSION_NAME);

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ToggleCategory(int? categoryId) {
            if (!Request.IsAjaxRequest() || categoryId == null) {
                return AccessDenied();
            }
            
            EditCategoriesModel.Current.IsAttached[categoryId.Value] = !EditCategoriesModel.Current.IsAttached[categoryId.Value];

            return PartialView("EditCategories", EditCategoriesModel.Current);
        }

    }
}
