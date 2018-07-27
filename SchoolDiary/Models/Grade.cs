using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class Grade
    {
        public int Id { get; set; }

        [Range(1, 5, ErrorMessage = ("Grade must have value between 1 and 5"))]
        public int GradeValue { get; set; }

        public DateTime GradeDate { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}