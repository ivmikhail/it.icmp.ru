using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using ITCommunity.DB;
using ITCommunity.DB.Tables;
using ITCommunity.Validators;


namespace ITCommunity.Models {

    public abstract class CaptchaModel {

        private Captcha captcha = new Captcha();

        public int QuestionId {
            get { return captcha.Id; }
            set { captcha.Id = value; }
        }

        public string Question {
            get { return captcha.Question; }
            set { captcha.Question = value; }
        }

        [CaptchaAnswer]
        public string AnswerId { get; set; }

        public IEnumerable<SelectListItem> Answers {
            get {
                var result =
                    from answer in captcha.CaptchaAnswers
                    select new SelectListItem { Text = answer.Text, Value = answer.Id.ToString() };

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
