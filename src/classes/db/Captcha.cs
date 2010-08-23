

namespace ITCommunity.DB {

    public partial class Captcha {

        partial void OnLoaded() {
            CaptchaAnswers.Load();
        }
    }
}
