using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Db;
using ITCommunity.Db.Tables;

namespace ITCommunity.Models {

    public class CaptchaEditModel {

        public int Id {
            get;
            set;
        }

        [Required(ErrorMessage = "Введите вопрос")]
        [DisplayName("Вопрос капчи")]
        public string Question { get; set; }

        public List<CaptchaAnswer> Answers {
            get {
                var captcha = Captchas.Get(Id);

                return new List<CaptchaAnswer>(captcha.CaptchaAnswers);
            }
        }

        [Required(ErrorMessage = "Выберите правильный ответ")]
        public int? RightAnswerId { get; set; }

        public CaptchaEditModel() {
        }

        public CaptchaEditModel(Captcha captcha) {
            Id = captcha.Id;
            Question = captcha.Question;
        }

        public Captcha ToCaptcha() {
            var captcha = Captchas.Get(Id);

            captcha.Question = Question;

            foreach (var answer in captcha.CaptchaAnswers) {
                if (answer.Id == RightAnswerId) {
                    answer.IsRight = true;
                } else {
                    answer.IsRight = false;
                }
            }

            return captcha;
        }
    }
}
