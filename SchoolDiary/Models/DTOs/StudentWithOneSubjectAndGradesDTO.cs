using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class StudentWithOneSubjectAndGradesDTO
    {
        public string StudentId { get; set; }

        public string StudentFirstName { get; set; }

        public string StudentLastName { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        public StudentWithOneSubjectAndGradesDTO()
        {
            Grades = new List<Grade>();
        }

        public StudentWithOneSubjectAndGradesDTO(string id, string studentName, string studentLastname)
        {
            StudentId = id;
            StudentFirstName = studentName;
            StudentLastName = studentLastname;
            Grades = new List<Grade>();
        }
    }
}