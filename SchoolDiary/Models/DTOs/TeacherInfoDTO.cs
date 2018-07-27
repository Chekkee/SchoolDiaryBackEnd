using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class TeacherInfoDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int SubjectFond { get; set; }

        public string MobilePhone { get; set; }

        public virtual ICollection<SubjectDisplayForTeacherDTO> SubjectsWithStudents { get; set; }

        public virtual ICollection<SubjectDisplayForTeacherDTO> RemainingSubjects { get; set; }

        public TeacherInfoDTO()
        {
            SubjectsWithStudents = new List<SubjectDisplayForTeacherDTO>();
            RemainingSubjects = new List<SubjectDisplayForTeacherDTO>();
        }

        public TeacherInfoDTO(string id, string firstName, string lastName, string username, string email, int subjectFond, string mobilePhone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Email = email;
            SubjectFond = subjectFond;
            MobilePhone = mobilePhone;
            SubjectsWithStudents = new List<SubjectDisplayForTeacherDTO>();
            RemainingSubjects = new List<SubjectDisplayForTeacherDTO>();
        }
    }
}