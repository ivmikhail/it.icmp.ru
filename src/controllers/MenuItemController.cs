using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Db.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class MenuItemController : BaseController {

        [Authorize(Roles = "admin")]
        public ActionResult List(int? id, int? parentid) {
            if (parentid != null) {
                return View(new MenuItemEditModel { ParentId = parentid.Value });
            }

            if (id == 0) {
                return View(new MenuItemEditModel());
            }

            var model = new MenuItemEditModel(id.Value);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult List(int? id, MenuItemEditModel model) {
            if (ModelState.IsValid) {
                var item = model.ToMenuItem();

                if (id == 0) {
                    MenuItems.Add(item);
                } else {
                    item.Id = id.Value;
                    MenuItems.Update(item);
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

            MenuItems.Delete(id.Value);

            return RedirectToAction("list");
        }
    }
}
