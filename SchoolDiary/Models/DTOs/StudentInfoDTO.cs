using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class StudentInfoDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public EYear Year { get; set; }

        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<ParentsInfoForStudentDTO> ParentsInfo { get; set; }

        public virtual ICollection<SubjectWithGradesDTO> SubjectsWithGrades { get; set; }

        public virtual ICollection<SubjectWithGradesDTO> RemainingSubjects { get; set; }

        public StudentInfoDTO()
        {
            ParentsInfo = new List<ParentsInfoForStudentDTO>();
            SubjectsWithGrades = new List<SubjectWithGradesDTO>();
            RemainingSubjects = new List<SubjectWithGradesDTO>();
        }

        public StudentInfoDTO(string id, string firstName, string lastName, string username, EYear year, DateTime dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Year = year;
            DateOfBirth = dateOfBirth;
            ParentsInfo = new List<ParentsInfoForStudentDTO>();
            SubjectsWithGrades = new List<SubjectWithGradesDTO>();
            RemainingSubjects = new List<SubjectWithGradesDTO>();
        }
    }
}