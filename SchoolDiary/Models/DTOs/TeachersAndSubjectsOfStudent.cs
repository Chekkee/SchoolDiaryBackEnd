using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class TeachersAndSubjectsOfStudent
    {
        public string TeacherId { get; set; }

        public string TeacherFirstName { get; set; }

        public string TeacherLastName { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public int SubjectFond { get; set; }

        public int SubjectYear { get; set; }

        public TeachersAndSubjectsOfStudent()
        {

        }

        public TeachersAndSubjectsOfStudent(string teacherId, string teacherFirstName, string teacherLastName, int subjectId, string subjectName, int subjectFond, int subjectYear)
        {
            TeacherId = teacherId;
            TeacherFirstName = teacherFirstName;
            TeacherLastName = teacherLastName;
            SubjectId = subjectId;
            SubjectName = subjectName;
            SubjectFond = subjectFond;
            SubjectYear = subjectYear;
        }
    }
}