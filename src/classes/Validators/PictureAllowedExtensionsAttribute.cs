using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

using ITCommunity.Core;
using System.IO;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class PictureAllowedExtensionsAttribute : ValidationAttribute {

        public static string AllowedExtensions {
            get { return Config.Get("PictureAllowedExtensions"); }
        }

        private const string _defaultErrorMessage = "Неверное расширение рисунка, вот список поддерживаемых расширений: {0}";

        public override string FormatErrorMessage(string name) {
            return String.Format(_defaultErrorMessage, AllowedExtensions);
        }

        public override bool IsValid(object value) {
            if (value == null)
                return true;


            var picture = (HttpPostedFileBase)value;
            var extension = Path.GetExtension(picture.FileName);

            var allowedExts = AllowedExtensions.Replace(" ", "").Split(',');

            foreach (var allowedExt in allowedExts) {
                if (extension.Equals(allowedExt)) {
                    return true;
                }
            }

            return false;
        }
    }
}
