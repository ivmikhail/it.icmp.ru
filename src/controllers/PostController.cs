﻿using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class PostController : PictureController {

        public ActionResult View(int? id) {
            var post = Posts.Get(id.Value);
            if (post == null) {
                return NotFound();
            }

            if (CurrentUser.User.Id != post.AuthorId) {
                Posts.IncViews(post);
            }

            return View("../" + post.EntityType + "/View", post);
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
            PostEditCategoriesModel.Current.Clear();
            var model = new PostEditModel();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(PostEditModel model) {
            if (Upload(model, Post.DefaultPicturesPath)) {
                return View(model);
            }

            if (ModelState.IsValid) {
                var post = model.ToPost();

                post = Posts.Add(post);

                if (CurrentUser.IsAdmin == false) {
                    var user = CurrentUser.User;
                    user.HeadersCounter++;
                    Users.Update(user);
                }

                post.Description = Picture.ReplaceUrls(Post.DefaultPicturesPath, post.PicturesPath, post.Description);
                post.Text = Picture.ReplaceUrls(Post.DefaultPicturesPath, post.PicturesPath, post.Text);
                Posts.Update(post, false);

                Picture.Clear(Post.DefaultPicturesPath);

                return RedirectToAction("view", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            var post = Posts.Get(id.Value);
            if (post == null) {
                return NotFound();
            }

            if (post.Editable == false) {
                return Forbidden();
            }

            var model = new PostEditModel(post);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, PostEditModel model) {
            var post = Posts.Get(id.Value);
            if (post == null) {
                return NotFound();
            }

            if (post.Editable == false) {
                return Forbidden();
            }

            if (Upload(model, post.PicturesPath)) {
                model.Path = post.PicturesPath;
                return View(model);
            }

            if (ModelState.IsValid) {
                var editedPost = model.ToPost();
                editedPost.Id = post.Id;
                Posts.Update(editedPost);

                Picture.DeleteUnused(editedPost.PicturesPath, editedPost.Description + editedPost.Text);

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public ActionResult ToggleCategory(int? categoryId) {
            if (!Request.IsAjaxRequest()) {
                return Forbidden();
            }

            if (categoryId == null) {
                return NotFound();
            }

            PostEditCategoriesModel.Current.IsAttached[categoryId.Value] = !PostEditCategoriesModel.Current.IsAttached[categoryId.Value];

            return PartialView("EditCategories", PostEditCategoriesModel.Current);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            Posts.Delete(id.Value);

            return Redirect("/");
        }
    }
}
