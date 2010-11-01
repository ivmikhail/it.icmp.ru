using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class RfcController : BaseController {

        [Authorize]
        public ActionResult Search(string q = "") {
            var model = new RfcListModel(q);
            return View("List", model);
        }
    }
}
