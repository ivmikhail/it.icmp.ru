using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ITCommunity.Core;
using ITCommunity.Db;
using ITCommunity.Db.Tables;

namespace ITCommunity.Controllers {

    public class CommentsController : Controller {

        public ActionResult Add(int? id, string addedcomment, int? captchaanswer, int? captchaquestion) {
            bool isright = true;
            isright = Captcha.IsRightAnswer(captchaanswer, captchaquestion);
            if (!CurrentUser.User.IsAnonymous)
                isright = true;

            if (isright) {
                Comment comment = new Comment();
                comment.UserId = CurrentUser.User.Id;
                comment.Ip = CurrentUser.Ip;
                comment.CreateDate = DateTime.Now;
                comment.PostId = id.Value;
                comment.Text = addedcomment;
                var comm = Comments.Add(comment);
                return Redirect("/posts/view/" + id + "#comment-" + comm.Id);
            }

            return Redirect("/posts/view/" + id + "#add-comment");
        }

    }
}
