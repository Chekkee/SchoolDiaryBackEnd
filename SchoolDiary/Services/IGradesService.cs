using SchoolDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface IGradesService
    {
        bool ExistsID(int id);

        Grade GetByID(int id);

        Grade PutGrade(int id, Grade grade);

        Grade DeleteByID(int id);
    }
}
