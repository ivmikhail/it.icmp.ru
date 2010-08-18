

namespace ITCommunity.Db {

    public partial class Captcha {

        partial void OnLoaded() {
            CaptchaAnswers.Load();
        }
    }
}
