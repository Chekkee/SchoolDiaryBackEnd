using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SchoolDiary.Validations
{
    sealed public class ValidatePasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            //Ovo sam nasao na netu, izgleda da prve dve cifre govore o duzini stringa koji se proverava, a ostatak kaze da string mora da ima bar jednu cifru, i bar jedno slovo
            Regex rgx = new Regex(@"^.*(?=.{4,15})(?=.*\d)(?=.*[a-zA-Z]).*$");
            if(rgx.IsMatch((string)value))
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