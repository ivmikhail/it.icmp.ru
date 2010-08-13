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

    public class FavoriteController : BaseController {

        public ActionResult Posts(int? page) {
            var model = new FavoritePostsModel(page);

            return View(model);
        }

        public ActionResult Add(int? id) {
            if (id == null) {
                return NotFound();
            }

            if (Db.Tables.Posts.Get(id.Value) == null) {
                return NotFound();
            }

            Favorite favorite = new Favorite();
            favorite.PostId = id.Value;
            favorite.UserId = CurrentUser.User.Id;
            favorite.CreateDate = DateTime.Now;

            Favorities.Add(favorite);

            return RedirectToAction("posts", "favorite");
        }

        public ActionResult Delete(int? id) {

            if (id != null && id > 0) {
                Favorities.Delete(id.Value, CurrentUser.User.Id);
            }
            return RedirectToAction("posts", "favorite");
        }
    }
}
