using ITCommunity.Db.Tables;

namespace ITCommunity.Db {

    public partial class Captcha {

        public static bool IsRightAnswer(int answerId, int questionId) {
            Captcha captcha = Captchas.Get(questionId);

            foreach (var answer in captcha.CaptchaAnswers) {
                if (answer.IsRight == 1 && answer.Id == answerId)
                    return true;
            }

            return false;
        }

        partial void OnLoaded() {
            CaptchaAnswers.Load();
        }
    }
}
