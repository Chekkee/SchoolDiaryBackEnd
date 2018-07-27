using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface ITeachersService
    {
        bool ExistsID(string id);
        bool ExistsUsername(string username);
        bool TeacherHasSubject(string id, int subjectId);
        bool TeacherHasStudent(string id, string studentId, int subjectId);

        IEnumerable<TeacherInfoDTO> Get();
        TeacherInfoDTO GetByID(string id);
        TeacherInfoDTO GetByUsername(string username);
        ICollection<SubjectDisplayForTeacherDTO> GetRemainingSubjects(string id);

        TeacherInfoDTO PutTeacher(string id, TeacherInfoDTO teacher);
        TeacherInfoDTO PutSubjectToTeacher(string id, int subjectId);
        TeacherInfoDTO PutStudentToTeacher(string id, string studentId, int subjectId);

        TeacherModel DeleteTeacher(string id);
        TeacherInfoDTO DeleteSubjectFromTeacher(string id, int subjectId);

        StudentModel PostGradeToStudent(string id, string studentId, int subjectId, Grade grade);
    }
}
