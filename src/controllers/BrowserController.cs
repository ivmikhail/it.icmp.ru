using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Models;


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
    }
}
