using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ITCommunity.Models;
using ITCommunity.Modules;

namespace ITCommunity.Controllers
{
    public class WinUpdatesController : BaseController
    {        
        public ActionResult Search(string start = "", string q1 = "", string q2 ="") {
            var model = new WinUpdatesListModel(start, q1, q2);
            return View("List", model);
        }

        public ActionResult File(string filename = "") {
            WsusFile file = null;
            if (filename != "") {
                file = Wsus.get(filename);
            }
            if (file != null) {
                HttpResponseBase response = ControllerContext.HttpContext.Response;
                response.AddHeader("content-disposition", "attachment; filename=" + file.Name);
                return base.File(file.RealPath, "application/octet-stream");
            } else {
                return NotFound();
            }
        }       
    }
}
