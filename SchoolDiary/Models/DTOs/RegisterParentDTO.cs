using SchoolDiary.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class RegisterParentDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Parent first name must be at least 2 letters long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Parent last name must be at least 2 letters long")]
        public string LastName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 15 characters long")]
        public string UserName { get; set; }

        [Required]
        [ValidatePassword(ErrorMessage = "Password must contain at least 1 letter and 1 number")]
        [StringLength(15, MinimumLength = 4, ErrorMessage = "Password must be between 4 and 15 letters long")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and confirmed password do not match")]
        public string RepeatedPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [ValidateMobilePhone]
        public string MobilePhone { get; set; }
    }
}