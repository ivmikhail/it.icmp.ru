using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class CheckCaptchaAttribute : ValidationAttribute {
        private const string _defaultErrorMessage = "Вы не правильно ответили на IT капчу";

        public string CaptchaId { get; private set; }

        public string AnswerId { get; private set; }

        public CheckCaptchaAttribute(string captchaId, string answerId)
            : base(_defaultErrorMessage) {
            CaptchaId = captchaId;
            AnswerId = answerId;
        }

        public override bool IsValid(object value) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            var captchaIdValue = properties.Find(CaptchaId, true /* ignoreCase */).GetValue(value);
            var answerIdValue = properties.Find(AnswerId, true /* ignoreCase */).GetValue(value);
            var captchaId = Convert.ToInt32(captchaIdValue);
            var answerId = Convert.ToInt32(answerIdValue);

            return Db.Captcha.IsRightAnswer(answerId, captchaId);
        }
    }
}
