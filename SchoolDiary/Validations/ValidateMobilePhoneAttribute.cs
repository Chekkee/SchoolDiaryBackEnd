using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SchoolDiary.Validations
{
    sealed public class ValidateMobilePhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string mobilePhoneNumber = (string)value;
            Regex rgx = new Regex(@"^06[0-5]{1}[0-9]{7}$");
            Regex rgx2 = new Regex(@"^063[0-9]{6}$");
            Regex rgx3 = new Regex(@"^\+3816[0-5]{1}[0-9]{7}$");
            Regex rgx4 = new Regex(@"^\+38163[0-9]{6}$");
            if (rgx.IsMatch(mobilePhoneNumber) || 
                rgx2.IsMatch(mobilePhoneNumber) || 
                rgx3.IsMatch(mobilePhoneNumber) || 
                rgx4.IsMatch(mobilePhoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString, name);
        }
    }
}