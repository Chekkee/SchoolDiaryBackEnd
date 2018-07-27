using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class ParentInfoDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        public virtual ICollection<StudentInfoDTO> StudentsWithSubjectsAndGrades { get; set; }

        public virtual ICollection<StudentInfoDTO> RemainingStudents { get; set; }

        public ParentInfoDTO()
        {
            StudentsWithSubjectsAndGrades = new List<StudentInfoDTO>();
            RemainingStudents = new List<StudentInfoDTO>();
        }

        public ParentInfoDTO(string id, string firstName, string lastName, string userName, string email, string mobilePhone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            MobilePhone = mobilePhone;
            StudentsWithSubjectsAndGrades = new List<StudentInfoDTO>();
            RemainingStudents = new List<StudentInfoDTO>();
        }
    }
}