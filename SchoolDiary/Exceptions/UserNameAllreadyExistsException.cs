using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Exceptions
{
    public class UserNameAllreadyExistsException : Exception
    {
        public UserNameAllreadyExistsException()
        {

        }

        public UserNameAllreadyExistsException(string message) : base(message)
        {

        }
    }
}