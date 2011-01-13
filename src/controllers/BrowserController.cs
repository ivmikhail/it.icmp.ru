﻿using System.Web.Mvc;

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

        [Authorize(Roles = "admin")]
        public ActionResult EditDesc(string link) {
            var item = BrowseItem.GetByLink(link);

            if (item != null) {
                return View(item);
            } else {
                return NotFound();
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditDesc(string link, string desc) {
            var item = BrowseItem.GetByLink(link);

            if (item == null || item.IsRoot) {
                return NotFound();
            }

            if (desc != null) {
                item.UpdateDesciption(desc.Trim());
            }

            return RedirectToAction("Files", new { link = item.Parent.RelativeLink });
        }
    }
}
