using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class SubjectDisplayForTeacherDTO
    {
        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public virtual ICollection<StudentWithOneSubjectAndGradesDTO> StudentsWithGrades { get; set; }

        public SubjectDisplayForTeacherDTO()
        {
            StudentsWithGrades = new List<StudentWithOneSubjectAndGradesDTO>();
        }

        public SubjectDisplayForTeacherDTO(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
            StudentsWithGrades = new List<StudentWithOneSubjectAndGradesDTO>();
        }
    }
}