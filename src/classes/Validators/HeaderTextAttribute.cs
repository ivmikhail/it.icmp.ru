using System;
using System.ComponentModel.DataAnnotations;

using ITCommunity.Core;
using ITCommunity.DB;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class HeaderTextAttribute : ValidationAttribute {

        private const string _maxLengthErrorMessage = "Вы написали {0} символов, а можно только {1}";
        private const string _requiredErrorMessage = "У Вас новых постов: {0} , а нужно {1}";

        private int _length;
        private bool _isRequiredError = false;

        public override string FormatErrorMessage(string name) {
            if (_isRequiredError) {
                return String.Format(_requiredErrorMessage, CurrentUser.User.HeadersCounter, Header.RequiredPostsCount);
            }

            return String.Format(_maxLengthErrorMessage, _length, Header.MaxLength);
        }

        public override bool IsValid(object value) {
            if (value == null)
                return false;

            if (CurrentUser.IsAdmin == false) {
                if (CurrentUser.User.HeadersCounter < Header.RequiredPostsCount) {
                    _isRequiredError = true;
                    return false;
                }
            }

            _length = value.ToString().Length;

            return _length <= Header.MaxLength;
        }
    }
}
