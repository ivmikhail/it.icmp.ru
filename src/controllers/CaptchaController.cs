using System.Collections.Generic;
using System.Web.Mvc;

using ITCommunity.Core;
using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Models;


namespace ITCommunity.Controllers {

    public class CaptchaController : BaseController {

        [Authorize(Roles="admin")]
        public ActionResult List(int? page) {
            var model = new CaptchaListModel(page);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Add(string Question) {
            if (Question != null) {
                var captcha = new Captcha();
                captcha.Question = Question;

                Captchas.Add(captcha);

                return RedirectToAction("edit", new { id = captcha.Id });
            }

            return Redirect();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id) {
            var captcha = Captchas.Get(id.Value);

            if (captcha == null) {
                return NotFound();
            }

            var model = new CaptchaEditModel(captcha);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(int? id, CaptchaEditModel model) {
            var captcha = Captchas.Get(id.Value);

            if (captcha == null) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                var editedCaptcha = model.ToCaptcha();

                Captchas.Update(editedCaptcha);

                return RedirectToAction("list");
            }

            return View(model);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id) {
            Captchas.Delete(id.Value);

            return RedirectToAction("list");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddAnswer(CaptchaAnswer answer) {
            if (Request.IsAjaxRequest() == false) {
                return Forbidden();
            }

            if (answer.Text != null) {
                Captchas.AddAnswer(answer);
            }

            var captcha = Captchas.Get(answer.CaptchaId);

            var answers = new List<CaptchaAnswer>(captcha.CaptchaAnswers);

            return PartialView("EditAnswers", answers);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAnswer(int? id) {
            if (Request.IsAjaxRequest() == false) {
                return Forbidden();
            }

            var captchaId =  Captchas.GetIdByAnswer(id.Value);

            Captchas.DeleteAnswer(id.Value);

            var captcha = Captchas.Get(captchaId);

            var answers = new List<CaptchaAnswer>(captcha.CaptchaAnswers);

            return PartialView("EditAnswers", answers);
        }
    }
}
