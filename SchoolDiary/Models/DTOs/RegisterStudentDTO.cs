using SchoolDiary.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class RegisterStudentDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Students name must be at least 2 letters long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Students last name must be at least 2 letters long")]
        public string LastName { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Students username must be at least 4 letters long")]
        public string UserName { get; set; }

        [Required]
        [ValidateDateRange(ErrorMessage ="Date of birth for new student must be within following dates: 1.1.2001 and 31.12.2013")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [ValidatePassword(ErrorMessage = "Password must contain at least one letter and one number")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 15 letters long")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and password confirmations do not match")]
        public string RepeatedPassword { get; set; }
    }
}