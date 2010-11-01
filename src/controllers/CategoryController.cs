using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class CategoryController : BaseController {

        public ActionResult Posts(int? id, int? page) {
            if (id == 0) {
                return NotFound();
            }

            var model = new CategoryPostsModel(id.Value, page);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult List(int? id) {
            if (id == 0) {
                return View();
            }

            var model = new CategoryEditModel(id.Value);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult List(int? id, CategoryEditModel model) {
            if (ModelState.IsValid) {
                var category = model.ToCategory();

                if (id == 0) {
                    Categories.Add(category);
                } else {
                    category.Id = id.Value;
                    Categories.Update(category);
                }

                return RedirectToAction("list", new { id = 0 });
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            if (id == 0) {
                return NotFound();
            }

            Categories.Delete(id.Value);

            return RedirectToAction("list");
        }
    }
}
