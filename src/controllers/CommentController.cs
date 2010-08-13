using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;
using ITCommunity.Models.Comment;

namespace ITCommunity.Controllers {

    public class CommentController : BaseController {

        public ActionResult Add(AddModel model) {
            if (!CurrentUser.isAuth) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                var comment = new Comment();
                comment.PostId = model.PostId;
                comment.Text = model.Text;
                comment = Comments.Add(comment);
                return Redirect("/post/view/" + comment.PostId + "#comment-" + comment.Id);
            }

            return View("AddPage", model);
        }

        public ActionResult AnonymousAdd(AnonymousAddModel model) {
            if (ModelState.IsValid) {
                var comment = new Comment();
                comment.PostId = model.PostId;
                comment.Text = model.Text;
                comment = Comments.Add(comment);
                return Redirect("/post/view/" + comment.PostId + "#comment-" + comment.Id);
            }

            model.NewCaptcha();
            return View("AnonymousAddPage", model);
        }
    }
}
