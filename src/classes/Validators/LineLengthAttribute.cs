﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace ITCommunity.Validators {

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class LineLengthAttribute : ValidationAttribute {

        private const string _defaultErrorMessage = "В одной строке может быть максимум {0} знаков";
        private int _maxLength;

        public LineLengthAttribute(int maxLength)
            : base(_defaultErrorMessage) {
            _maxLength = maxLength;
        }

        public override bool IsValid(object value) {
            if (value == null)
                return true;

            return value.ToString().Length <= _maxLength;
        }

        public override string FormatErrorMessage(string name) {
            return String.Format(_defaultErrorMessage, _maxLength);
        }
    }
}
