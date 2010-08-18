using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Db.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class CommentController : BaseController {

        [HttpPost]
        public ActionResult AnonymousAdd(AnonymousCommentAddModel model) {
            if (ModelState.IsValid) {
                var comment = model.ToComment();

                comment = Comments.Add(comment);

                return Redirect("/post/view/" + comment.PostId + "#comment-" + comment.Id);
            }

            model.NewCaptcha();
            return View("AnonymousAddPage", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(CommentEditModel model) {
            if (ModelState.IsValid) {
                var comment = model.ToComment();

                comment = Comments.Add(comment);

                return Redirect("/post/view/" + comment.PostId + "#comment-" + comment.Id);
            }

            return View("AddPage", model);
        }

        [Authorize]
        public ActionResult Edit(int? id) {
            if (id == 0) {
                return NotFound();
            }

            var comment = Comments.Get(id.Value);

            if (comment == null) {
                return NotFound();
            }

            if (!comment.Editable) {
                return AccessDenied();
            }

            return View("EditPage", new CommentEditModel(comment));
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int? id, CommentEditModel model) {
            if (id == 0) {
                return NotFound();
            }

            var comment = Comments.Get(id.Value);

            if (comment == null) {
                return NotFound();
            }

            if (!comment.Editable) {
                return AccessDenied();
            }

            if (ModelState.IsValid) {
                var editedComment = model.ToComment();

                editedComment.Id = id.Value;
                Comments.Update(editedComment);

                return Redirect("/post/view/" + comment.PostId + "#comment-" + comment.Id);
            }

            return View("EditPage", model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Comments.Delete(id.Value);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
