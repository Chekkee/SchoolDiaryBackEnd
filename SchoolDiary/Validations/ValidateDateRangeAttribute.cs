using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SchoolDiary.Validations
{
    public class ValidateDateRangeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if ((DateTime)value >= new DateTime(2001, 1, 1) && (DateTime)value <= new DateTime(2013, 12, 21))
            {
                return true;
            }
            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}