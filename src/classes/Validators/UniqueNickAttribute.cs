using System;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB.Tables;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class UniqueNickAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Такой ник уже используется.";

        public UniqueNickAttribute()
            : base(_defaultErrorMessage) {
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            var user = Users.Get(value.ToString());
            return user.IsAnonymous;
        }
    }
}
