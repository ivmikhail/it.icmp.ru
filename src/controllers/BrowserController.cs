using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Models;
using System.Web;


namespace ITCommunity.Controllers {

    public class BrowserController : BaseController {

        [Authorize]
        public ActionResult Files(string link) {
            var item = BrowseItem.GetByLink(link);

            if (item != null) {
                return View(item);
            } else {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditDesc(string path) {
            var link = System.Uri.UnescapeDataString(path);
            var item = BrowseItem.GetByLink(link);

            if (item != null) {
                return View(item);
            } else {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditDesc(string path, string desc) {
            var link = System.Uri.UnescapeDataString(path);
            var item = BrowseItem.GetByLink(link);

            if (item == null || item.IsRoot) {
                return NotFound();
            }

            if (desc != null) {
                item.UpdateDesciption(HttpUtility.HtmlEncode(desc));
            }

            return RedirectToAction("Files", new { link = item.Parent.RelativeLink });
        }
    }
}
