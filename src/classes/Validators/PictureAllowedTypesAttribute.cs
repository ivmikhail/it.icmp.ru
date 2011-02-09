using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

using ITCommunity.Core;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class PictureAllowedTypesAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Неверный формат рисунка, вот список поддерживаемых форматов: {0}";

        public override string FormatErrorMessage(string name) {
            return String.Format(_defaultErrorMessage, Picture.AllowedTypes);
        }

        public override bool IsValid(object value) {
            if (value == null)
                return true;


            var picture = (HttpPostedFileBase)value;

            var typesArray = Picture.AllowedTypes.Replace(" ", "").Split(',');

            foreach (var type in typesArray) {
                if (picture.ContentType.Equals(type)) {
                    return true;
                }
            }

            return false;
        }
    }
}
