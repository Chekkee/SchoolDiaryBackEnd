using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class TeacherModel : UserModel
    {
        public int SubjectFond { get; set; }

        public virtual ICollection<TeacherToSubject> TeacherToSubjects { get; set; }

        public TeacherModel()
        {
            TeacherToSubjects = new List<TeacherToSubject>();
        }
    }
}