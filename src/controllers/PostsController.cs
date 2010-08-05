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

        public ActionResult Category(int? id, int? page) {
            if (id == null) {
                return NotFound();
            }

            var model = new CategoryPostsModel(id.Value, page);

            return View(model);
        }

        public ActionResult View(int? id) {
            if (id == null) {
                return NotFound();
            }

            var model = Posts.Get(id.Value);
            if (model != null) {
                Posts.IncView(id.Value);
                model.ViewsCount++;
                return View(model);
            }

            return NotFound();
        }
        
        #region Favorite

        public ActionResult Favorite(int? page) {
            var model = new FavoritePostsModel(page);

            return View(model);
        }

        public ActionResult AddFavorite(int? id) {
            if (id == null) {
                return NotFound();
            }

            if (Posts.Get(id.Value) == null) {
                return NotFound();
            }

            Favorite favorite = new Favorite();
            favorite.PostId = id.Value;
            favorite.UserId = CurrentUser.User.Id;
            favorite.CreateDate = DateTime.Now;

            Favorities.Add(favorite);

            return RedirectToAction("favorite", "posts");
        }

        public ActionResult DeleteFavorite(int? id) {

            if (id != null && id > 0) {
                Favorities.Delete(id.Value, CurrentUser.User.Id);
            }
            return RedirectToAction("favorite", "posts");
        }

        #endregion

        public ActionResult Add() {
            return View(new AddPostModel());
        }

        [HttpPost]
        public ActionResult Add(AddPostModel addingPost, int[] categoriesIds) {
            var post = new Post();
            post.AuthorId = CurrentUser.User.Id;
            post.Title = addingPost.Title;
            post.Description = addingPost.Description;
            post.Text = addingPost.Text;
            post.Source = addingPost.Source;

            foreach (var catId in addingPost.CategoriesIds) {
                post.Categories.Add(Categories.Get(catId));
            }

            post = Posts.Add(post);
            return RedirectToAction("view", "posts", new { id = post.Id });
        }

        [HttpGet]
        public ActionResult Search(string query)
        {
            return View();
        }

    }
}
