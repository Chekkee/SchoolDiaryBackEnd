using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface IAdminsService
    {
        bool ExistsID(string id);

        IEnumerable<AdminModel> GetAll();
        AdminModel GetByID(string id);
        ICollection<string> GetLogs();

        StudentInfoDTO PostGradeToStudent(string studentId, int subjectId, Grade grade);
    }
}
