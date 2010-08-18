using System;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Db.Tables;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CaptchaAnswerAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Вы не правильно ответили на IT капчу";

        public CaptchaAnswerAttribute()
            : base(_defaultErrorMessage) {
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            int answerId;
            if (int.TryParse(value.ToString(), out answerId)) {
                return Captchas.IsRightAnswer(answerId);
            }

            return false;
        }
    }
}
