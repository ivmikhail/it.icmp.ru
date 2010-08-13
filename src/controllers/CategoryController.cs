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

    public class CategoryController : BaseController {

        public ActionResult Posts(int? id, int? page) {
            if (id == null) {
                return NotFound();
            }

            var model = new CategoryPostsModel(id.Value, page);

            return View(model);
        }
    }
}
