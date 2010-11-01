using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class FavoriteController : BaseController {

        [Authorize]
        public ActionResult Posts(int? page) {
            var model = new FavoritePostsModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult Add(int? id) {
            if (id == 0) {
                return NotFound();
            }

            if (DB.Tables.Posts.Get(id.Value) == null) {
                return NotFound();
            }

            Favorite favorite = new Favorite();
            favorite.PostId = id.Value;
            favorite.UserId = CurrentUser.User.Id;

            Favorites.Add(favorite);

            return RedirectToAction("posts");
        }

        [Authorize]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Favorites.Delete(id.Value, CurrentUser.User.Id);

            return RedirectToAction("posts");
        }
    }
}
