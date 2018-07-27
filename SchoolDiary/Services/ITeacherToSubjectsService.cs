using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface ITeacherToSubjectsService
    {
        TeacherModel PutSubjectToTeacher(int id, int subjectId);
    }
}
