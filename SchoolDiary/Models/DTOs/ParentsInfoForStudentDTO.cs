using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models.DTOs
{
    public class ParentsInfoForStudentDTO
    {
        public string ParentId { get; set; }

        public string ParentFirstName { get; set; }

        public string ParentLastName { get; set; }

        public ParentsInfoForStudentDTO(string parentId, string parentFirstName, string parentLastName)
        {
            ParentId = parentId;
            ParentFirstName = parentFirstName;
            ParentLastName = parentLastName;
        }
    }
}