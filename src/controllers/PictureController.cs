using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Models;

namespace ITCommunity.Controllers {

    public class PictureController : BaseController {

        public bool Upload(PictureUploadModel model, string basePath) {
            if (Request["UploadPicture"] == null) {
                return false;
            }

            if (ModelState.IsValidField("Picture") && model.Picture != null) {
                Picture.Upload(model.Picture, basePath);
            }

            return true;
        }
    }
}
