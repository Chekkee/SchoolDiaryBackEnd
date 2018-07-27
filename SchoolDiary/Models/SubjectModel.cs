using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class SubjectModel
    {
        public int Id { get; set; }

        public string SubjectName { get; set; }

        public int SubjectFond { get; set; }

        public virtual EYear Year { get; set; }
     
        [JsonIgnore]
        public virtual ICollection<StudentToSubject> StudentToSubjects { get; set; }

        [JsonIgnore]
        public virtual ICollection<TeacherToSubject> TeacherToSubjects { get; set; }

        public SubjectModel()
        {
            StudentToSubjects = new List<StudentToSubject>();
            TeacherToSubjects = new List<TeacherToSubject>();
        }
    }
}