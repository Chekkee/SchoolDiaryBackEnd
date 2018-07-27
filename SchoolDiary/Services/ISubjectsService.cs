using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface ISubjectsService
    {
        bool ExistsID(int id);
        bool ExistsSubjectName(string subjectName);
        IEnumerable<SubjectModel> GetAllSubjects();
        SubjectModel GetByID(int id);
        SubjectModel GetBySubjectName(string subjectName);
        IEnumerable<TeacherInfoDTO> GetPossibleTeachers(int id);

        SubjectModel PostSubject(SubjectModel subject);

        SubjectModel PutSubject(int id, SubjectModel subject);

        SubjectModel DeleteByID(int id);
    }
}
