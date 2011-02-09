using System.Drawing;
using System.IO;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public abstract class BasePostController : BasePictureController {

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

        public virtual ActionResult BaseAdd() {
            PostEditCategoriesModel.Current.Clear();
            var model = new PostEditModel();
            return View(model);
        }

        public virtual ActionResult BaseAdd(PostEditModel model) {
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

                Picture.MoveAll(Post.DefaultPicturesPath, post.PicturesPath);
                Picture.Clear(Post.DefaultPicturesPath);

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public virtual ActionResult BaseEdit(int? id) {
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
        public virtual ActionResult BaseEdit(int? id, PostEditModel model) {
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

                Picture.DeleteUnused(post.PicturesPath, editedPost.Description + editedPost.Text);

                return RedirectToAction("view", "post", new { id = post.Id });
            }

            return View(model);
        }

        [Authorize]
        public virtual ActionResult ToggleCategory(int? categoryId) {
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
        public virtual ActionResult Delete(int? id) {
            Posts.Delete(id.Value);

            return Redirect("/");
        }
    }
}
