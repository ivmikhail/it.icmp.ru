using System;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB.Tables;
using ITCommunity.Core;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CurrentUserPasswordAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Вы неправильно ввели свой текущий пароль.";

        public CurrentUserPasswordAttribute()
            : base(_defaultErrorMessage) {
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            var user = Users.Get(CurrentUser.User.Id);

            var hashedPassword = CurrentUser.HashPassword(value.ToString(), user.Nick);

            return hashedPassword.Equals(CurrentUser.User.Password);
        }
    }
}
