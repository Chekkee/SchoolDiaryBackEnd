using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public abstract class UserModel : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EUserRole UserRole { get; set; }
    }
}