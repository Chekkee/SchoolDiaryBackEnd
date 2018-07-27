using NLog;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SchoolDiary.Services
{
    public class StudentsService : IStudentsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public StudentsService(IUnitOfWork db, ISubjectsService subjectsService)
        {
            this.db = db;
        }

        public bool ExistsID(string id)
        {
            string Id = id.ToString();
            if(db.StudentsRepository.Get().FirstOrDefault(x=>x.Id == Id) == null)
            {
                return false;
            }
            return true;
        }

        public bool IsInDB(string username)
        {
            if(db.StudentsRepository.Get().FirstOrDefault(x=>x.UserName == username) == null)
            {
                return false;
            }
            return true;
        }

        public bool StudentHasSubject(string Id, int subjectId)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(Id.ToString());
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);

            if(StudentFromDB.StudentToSubjects.FirstOrDefault(x=>x.Subject == SubjectFromDB) == null)
            {
                return false;
            }
            return true;
        }

        public bool StudentHasParent(string id, string parentId)
        {
            StudentModel sm = db.StudentsRepository.GetByID(id.ToString());

            if(sm.Parents.FirstOrDefault(x=>x.Id == parentId.ToString()) == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<StudentInfoDTO> Get()
        {
            IEnumerable<StudentModel> allStudents = db.StudentsRepository.Get();
            ICollection<StudentInfoDTO> studentsDTOs = new List<StudentInfoDTO>();
            foreach(StudentModel student in allStudents)
            {
                studentsDTOs.Add(MakeStudentInfoDTO(student));
            }
            return studentsDTOs;
        }

        public StudentInfoDTO GetByID(string id)
        {
            StudentModel studentFromDB = db.StudentsRepository.GetByID(id);
            return MakeStudentInfoDTO(studentFromDB);
        }

        public StudentInfoDTO GetByUsername(string username)
        {
            StudentModel StudentFromDB = db.StudentsRepository.Get().FirstOrDefault(x => x.UserName == username);
            return MakeStudentInfoDTO(StudentFromDB);
        }

        public ICollection<SubjectWithGradesDTO> GetAllSubjectsWithGrades(string id)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(id);

            return GetStudentsSubjectsWithGradesDTO(StudentFromDB);
        }

        public IEnumerable<SubjectWithGradesDTO> GetSubjectsThatStudenDoesNotHave(string id)
        {

            StudentModel student = db.StudentsRepository.GetByID(id);

            IEnumerable<SubjectModel> remainingSubjects = db.SubjectsRepository.Get().Except(student.StudentToSubjects.Select(x => x.Subject));

            ICollection<SubjectWithGradesDTO> remainingDTOs = new List<SubjectWithGradesDTO>();

            foreach(SubjectModel subjects in remainingSubjects)
            {
                remainingDTOs.Add(new SubjectWithGradesDTO(subjects.Id, subjects.SubjectName));
            }

            return remainingDTOs;
        }

        public ICollection<TeachersAndSubjectsOfStudent> GetStudentsTeachersWithSubjects(string id)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(id);
            ICollection<TeachersAndSubjectsOfStudent> teachersAndSubjects = new List<TeachersAndSubjectsOfStudent>();

            foreach(StudentToSubject sts in StudentFromDB.StudentToSubjects)
            {
                TeachersAndSubjectsOfStudent teachersWithSubjects = new TeachersAndSubjectsOfStudent(
                    sts.TeacherToSubject.Teacher.Id,
                    sts.TeacherToSubject.Teacher.FirstName,
                    sts.TeacherToSubject.Teacher.LastName,
                    sts.Subject.Id,
                    sts.Subject.SubjectName,
                    sts.Subject.SubjectFond,
                    (int)sts.Subject.Year);

                teachersAndSubjects.Add(teachersWithSubjects);
            }

            return teachersAndSubjects;
        }

        public StudentInfoDTO PutStudent(string id, StudentInfoDTO student)
        {
            StudentModel studentFromDB = db.StudentsRepository.GetByID(id);
            studentFromDB.FirstName = student.FirstName;
            studentFromDB.LastName = student.LastName;
            studentFromDB.UserName = student.UserName;
            studentFromDB.Year = student.Year;
            studentFromDB.DateOfBirth = student.DateOfBirth;

            db.StudentsRepository.Update(studentFromDB);
            db.Save();

            logger.Info($"Information about student with and Id of {id} were changed");

            return MakeStudentInfoDTO(studentFromDB);
        }

        public StudentInfoDTO PutParentToStudent(string Id, string ParentId)
        {
            StudentModel studentFromDB = db.StudentsRepository.GetByID(Id);
            ParentModel parentFromDB = db.ParentsRepository.GetByID(ParentId);

            studentFromDB.Parents.Add(parentFromDB);
            db.StudentsRepository.Update(studentFromDB);
            db.Save();

            logger.Info($"Parent with id {ParentId} ({parentFromDB.FirstName} {parentFromDB.LastName}) registered student with id {Id} ({studentFromDB.FirstName} {studentFromDB.LastName}) as his/her child");

            return MakeStudentInfoDTO(studentFromDB);
        }

        public StudentInfoDTO PutSubjectToStudent(string Id, int subjectId)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(Id);
            SubjectModel sub = db.SubjectsRepository.GetByID(subjectId);
            StudentToSubject sts = new StudentToSubject()
            {
                Student = StudentFromDB,
                Subject = sub
            };

            StudentFromDB.StudentToSubjects.Add(sts);
            db.StudentsRepository.Update(StudentFromDB);
            db.Save();

            logger.Info($"Student with id: {Id} ({StudentFromDB.FirstName} {StudentFromDB.LastName}, with username: {StudentFromDB.UserName}) received new subject with id: {subjectId} ({sub.SubjectName})");

            return MakeStudentInfoDTO(StudentFromDB);
        }

        public StudentModel DeleteStudent(string id)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(id);
            db.StudentsRepository.Delete(StudentFromDB);
            db.Save();

            logger.Warn($"Student with id: {id} was deleted");

            return StudentFromDB;
        }

        public StudentInfoDTO DeleteSubjectFromStudent(string id, int subjectId)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(id.ToString());
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);
            StudentToSubject sts = db.StudentToSubjectsRepository.Get().FirstOrDefault(x => x.Student == StudentFromDB && x.Subject == SubjectFromDB);

            StudentFromDB.StudentToSubjects.Remove(sts);
            db.StudentsRepository.Update(StudentFromDB);
            db.Save();

            logger.Info($"Subject with id: {subjectId} ({SubjectFromDB.SubjectName}) was removed from student with id: {id} ({StudentFromDB.FirstName} {StudentFromDB.LastName} with username: {StudentFromDB.UserName})");

            return MakeStudentInfoDTO(StudentFromDB);
        }

        public StudentInfoDTO DeleteParentFromStudent(string id, string parentId)
        {
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(id.ToString());
            ParentModel ParentFromDB = db.ParentsRepository.GetByID(parentId.ToString());

            StudentFromDB.Parents.Remove(ParentFromDB);
            db.StudentsRepository.Update(StudentFromDB);
            db.Save();

            logger.Info($"Student with id {id} ({StudentFromDB.FirstName} {StudentFromDB.LastName}) is no longer registered child of parent with id {parentId} ({ParentFromDB.FirstName} {ParentFromDB.LastName})");

            return MakeStudentInfoDTO(StudentFromDB);
        }

        public StudentInfoDTO MakeStudentInfoDTO(StudentModel studentFromDB)
        {
            StudentInfoDTO student = new StudentInfoDTO(studentFromDB.Id, studentFromDB.FirstName, studentFromDB.LastName, studentFromDB.UserName, studentFromDB.Year, studentFromDB.DateOfBirth);
            foreach (ParentModel parent in studentFromDB.Parents)
            {
                ParentsInfoForStudentDTO parentsInfo = new ParentsInfoForStudentDTO(parent.Id, parent.FirstName, parent.LastName);
                student.ParentsInfo.Add(parentsInfo);
            }

            foreach (StudentToSubject sts in studentFromDB.StudentToSubjects)
            {
                SubjectWithGradesDTO subject = new SubjectWithGradesDTO(sts.Subject.Id, sts.Subject.SubjectName);
                
                foreach (Grade grade in sts.Grades)
                {
                    subject.Grades.Add(grade);
                }
                student.SubjectsWithGrades.Add(subject);
            }

            return student;
        }

        public ICollection<SubjectWithGradesDTO> GetStudentsSubjectsWithGradesDTO(StudentModel StudentFromDb)
        {
            ICollection<SubjectWithGradesDTO> subjectsWithGrades = new List<SubjectWithGradesDTO>();

            foreach (StudentToSubject sts in StudentFromDb.StudentToSubjects)
            {
                SubjectWithGradesDTO subjectWithGradesDTO = new SubjectWithGradesDTO(sts.Subject.Id, sts.Subject.SubjectName);

                foreach (Grade grade in sts.Grades)
                {
                    subjectWithGradesDTO.Grades.Add(grade);
                }

                subjectsWithGrades.Add(subjectWithGradesDTO);
            }

            return subjectsWithGrades;
        }

        public IEnumerable<TeacherModel> GetTeachersWithSubject(int id)
        {
            SubjectModel subject = db.SubjectsRepository.GetByID(id);

            IEnumerable<TeacherModel> teachersWithThatSubject = subject.TeacherToSubjects.Select(x => x.Teacher);

            return teachersWithThatSubject;
        }
    }
}