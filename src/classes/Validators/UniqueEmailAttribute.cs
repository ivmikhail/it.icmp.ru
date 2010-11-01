using System;
using System.ComponentModel.DataAnnotations;

using ITCommunity.DB.Tables;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class UniqueEmailAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Такая почта уже используется.";

        public UniqueEmailAttribute()
            : base(_defaultErrorMessage) {
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            var user = Users.GetByEmail(value.ToString());
            return user.IsAnonymous;
        }
    }
}
