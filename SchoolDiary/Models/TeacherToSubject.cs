using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class TeacherToSubject
    {
        public int Id { get; set; }

        public string TeacherId { get; set; }

        public virtual TeacherModel Teacher { get; set; }

        public int SubjectId { get; set; }

        public virtual SubjectModel Subject { get; set; }

        public virtual ICollection<StudentToSubject> StudentToSubjects { get; set; }

        public TeacherToSubject()
        {
            StudentToSubjects = new List<StudentToSubject>();
        }
    }
}