using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class StudentToSubject
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public virtual StudentModel Student { get; set; }

        public int SubjectId { get; set; }

        public virtual SubjectModel Subject { get; set; }

        public virtual TeacherToSubject TeacherToSubject { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        public StudentToSubject()
        {
            Grades = new List<Grade>();
        }
    }
}