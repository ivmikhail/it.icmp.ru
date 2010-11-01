using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class HeaderController : BaseController {

        [Authorize(Roles="admin")]
        public ActionResult List(int? page) {
            var model = new HeaderListModel(page);

            return View(model);
        }

        [Authorize]
        public ActionResult Add() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(HeaderAddModel model) {
            if (ModelState.IsValid) {
                var header = model.ToHeader();

                Headers.Add(header);

                if (CurrentUser.IsAdmin == false) {
                    var user = CurrentUser.User;
                    user.HeadersCounter = 0;
                    Users.Update(user);
                }

                return View("added");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Stop(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Headers.Stop(id.Value);

            return RedirectToAction("list");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Show(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Headers.Show(id.Value);

            return RedirectToAction("list");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Headers.Delete(id.Value);

            return RedirectToAction("list");
        }
    }
}
