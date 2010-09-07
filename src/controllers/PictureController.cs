using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.Models;

namespace ITCommunity.Controllers {

    public class PictureController : BaseController {

        private const string MaxSizeError = "Загружаемый рисунок слишком большой, максимальный размер файла {0}";
        private const string TypeError = "Не верный формат рисунка, вот список поддерживаемых форматов: {0}";
        private const string RequiredError = "Выберите рисунок";

        public static int MaxSize {
            get { return Config.GetInt("PictureMaxSize"); }
        }

        public static string Types {
            get { return Config.Get("PictureContentTypes"); }
        }

        public bool IsValidSize(int size) {
            return size <= MaxSize;
        }

        public bool IsValidType(string contentType) {
            var typesArray = Types.Replace(" ", "").Split(',');

            foreach (var type in typesArray) {
                if (contentType.Equals(type)) {
                    return true;
                }
            }

            return false;
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase picture) {
            bool isValid = true;
            if (picture == null) {
                isValid = false;
                ModelState.AddModelError("", RequiredError);
            }

            if (isValid && IsValidSize(picture.ContentLength) == false) {
                isValid = false;
                ModelState.AddModelError("", string.Format(MaxSizeError, MaxSize));
            }

            if (isValid && IsValidType(picture.ContentType) == false) {
                isValid = false;
                ModelState.AddModelError("", string.Format(TypeError, Types));
            }

            if (isValid) {
                Picture.Upload(picture, Config.Get("PicturePostsFolder"));
            }

            return PartialView();
        }
    }
}
