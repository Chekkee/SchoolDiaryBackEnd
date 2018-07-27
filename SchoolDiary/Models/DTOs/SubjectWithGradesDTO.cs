using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class SubjectWithGradesDTO
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        public SubjectWithGradesDTO(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            Grades = new List<Grade>();
        }

    }
}