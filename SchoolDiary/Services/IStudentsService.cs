using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolDiary.Services
{
    public interface IStudentsService
    {
        bool ExistsID(string id);
        bool IsInDB(string username);
        bool StudentHasSubject(string id, int subjectId);
        bool StudentHasParent(string id, string parentId);

        IEnumerable<StudentInfoDTO> Get();
        StudentInfoDTO GetByID(string id);
        StudentInfoDTO GetByUsername(string username);
        ICollection<SubjectWithGradesDTO> GetAllSubjectsWithGrades(string id);
        IEnumerable<SubjectWithGradesDTO> GetSubjectsThatStudenDoesNotHave(string id);
        ICollection<TeachersAndSubjectsOfStudent> GetStudentsTeachersWithSubjects(string id);

        StudentInfoDTO PutStudent(string id, StudentInfoDTO student);
        StudentInfoDTO PutParentToStudent(string id, string parentId);
        StudentInfoDTO PutSubjectToStudent(string id, int subjectId);

        StudentModel DeleteStudent(string Id);
        StudentInfoDTO DeleteSubjectFromStudent(string id, int subjectId);
        StudentInfoDTO DeleteParentFromStudent(string id, string parentId);

        StudentInfoDTO MakeStudentInfoDTO(StudentModel student);
    }
}
