using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class EmailAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Введите корректный e-mail.";

        public EmailAttribute()
            : base(_defaultErrorMessage) {
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            return Regex.IsMatch(value.ToString(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }
    }
}
