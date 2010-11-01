using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

using ITCommunity.Core;
using ITCommunity.Utils;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class PictureMaxSizeAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "Максимальный размер рисунка может быть {0}, а размер Вашего файла {1}";

        private int _size;

        public override string FormatErrorMessage(string name) {
            return String.Format(_defaultErrorMessage, HtmlExtension.FileSize(null, Picture.MaxSize), HtmlExtension.FileSize(null, _size));
        }

        public override bool IsValid(object value) {
            if (value == null)
                return true;


            var picture = (HttpPostedFileBase)value;

            _size = picture.ContentLength;

            return _size <= Picture.MaxSize;
        }
    }
}
