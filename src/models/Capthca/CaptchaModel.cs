using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using ITCommunity.Db.Tables;
using ITCommunity.Validators;


namespace ITCommunity.Models.Captcha {

    [CheckCaptcha("QuestionId", "AnswerId")]
    public abstract class CaptchaModel {

        private Db.Captcha captcha = new Db.Captcha();

        public int QuestionId {
            get {
                return captcha.Id;
            }
            set {
                captcha.Id = value;
            }
        }

        public string Question {
            get {
                return captcha.Question;
            }
            set {
                captcha.Question = value;
            }
        }

        public string AnswerId { get; set; }

        public IEnumerable<SelectListItem> Answers {
            get {
                var result = from answer in captcha.CaptchaAnswers
                             select new SelectListItem {
                                 Text = answer.Text,
                                 Value = answer.Id.ToString()
                             };

                return result;
            }
        }

        public CaptchaModel() {
            NewCaptcha();
        }

        public void NewCaptcha() {
            captcha = Captchas.GetRandom();
        }
    }
}
