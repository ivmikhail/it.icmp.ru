using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

using ITCommunity.Db.Tables;

namespace ITCommunity.Models {

    #region Models

    public class NickModel {

        [Required(ErrorMessage = "Введите Ваш ник")]
        [DisplayName("Ваш ник")]
        public string UserNick { get; set; }

    }


    [PropertiesMustMatch("NewPassword", "ConfirmPassword")]
    public class NewPasswordModel : NickModel {

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Новый пароль")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Повтор паролья")]
        public string ConfirmPassword { get; set; }

    }


    public class LoginModel : NickModel {

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Запомнить?")]
        public bool RememberMe { get; set; }

    }


    [PropertiesMustMatch("Password", "ConfirmPassword")]
    public class RegisterModel {

        [UniqueNick]
        [Required(ErrorMessage = "Введите Ваш ник")]
        [DisplayName("Ваш ник")]
        public string UserNick { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email адресс")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите повтор пароля")]
        [DataType(DataType.Password)]
        [DisplayName("Повтор паролья")]
        public string ConfirmPassword { get; set; }

    }

    #endregion
    
    #region Validation

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "'{0}' и '{1}' не совпадают.";

        private readonly object _typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(_defaultErrorMessage) {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }

        public string OriginalProperty { get; private set; }

        public override object TypeId {
            get { return _typeId; }
        }

        public override string FormatErrorMessage(string name) {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value) {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }
    }


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute {
        private const string _defaultErrorMessage = "'{0}' must be at least {1} characters long.";

        private readonly int _minCharacters = 1;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage) {
        }

        public override string FormatErrorMessage(string name) {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value) {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }


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
            return (user.IsAnonymous);
        }
    }


    #endregion

}
