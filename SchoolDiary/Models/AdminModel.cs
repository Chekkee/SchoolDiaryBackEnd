using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class AdminModel : UserModel
    {
        public string AdminRight { get; set; }
    }
}