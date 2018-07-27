using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface IParentsService
    {
        bool ExistsID(string Id);
        bool ExistsUsername(string username);

        IEnumerable<ParentInfoDTO> Get();
        ParentInfoDTO GetByID(string Id);
        ParentInfoDTO GetByUsername(string username);
        ICollection<StudentInfoDTO> GetRemainingStudents(string id);

        ParentInfoDTO PutParent(string id, ParentInfoDTO parent);

        ParentModel DeleteParent(string Id);

    }
}
