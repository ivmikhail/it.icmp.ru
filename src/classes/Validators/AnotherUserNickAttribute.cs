using System;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Db.Tables;
using ITCommunity.Core;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class AnotherUserNickAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Должен быть ник другого пользователя.";

        public AnotherUserNickAttribute()
            : base(_defaultErrorMessage) {
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            var user = Users.Get(value.ToString());

            return !user.Nick.EndsWith(CurrentUser.User.Nick, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
