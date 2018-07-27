using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Models
{
    public class ParentModel : UserModel
    {
        public virtual ICollection<StudentModel> Students { get; set; }

        public ParentModel()
        {
            Students = new List<StudentModel>();
        }
    }
}