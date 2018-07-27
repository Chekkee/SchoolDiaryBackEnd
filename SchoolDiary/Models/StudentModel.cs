using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class StudentModel : UserModel
    {
        public string PicturePath { get; set; }

        public virtual DateTime DateOfBirth { get; set; }

        public virtual EYear Year { get; set; }

        public virtual ICollection<ParentModel> Parents { get; set; }

        public virtual ICollection<StudentToSubject> StudentToSubjects { get; set; }

        public StudentModel()
        {
            Parents = new List<ParentModel>();
            StudentToSubjects = new List<StudentToSubject>();
        }
    }
}