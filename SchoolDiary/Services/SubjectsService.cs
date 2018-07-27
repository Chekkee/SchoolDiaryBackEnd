using NLog;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Services
{
    public class SubjectsService : ISubjectsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public SubjectsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public bool ExistsID(int Id)
        {
            if(db.SubjectsRepository.GetByID(Id) == null)
            {
                return false;
            }
            return true;
        }

        public bool ExistsSubjectName(string subjectName)
        {
            if(db.SubjectsRepository.Get().FirstOrDefault(x=>x.SubjectName == subjectName) == null)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<SubjectModel> GetAllSubjects()
        {
            return db.SubjectsRepository.Get();
        }

        public SubjectModel GetByID(int Id)
        {
            return db.SubjectsRepository.GetByID(Id);
        }

        public SubjectModel GetBySubjectName(string subjectName)
        {
            return db.SubjectsRepository.Get().FirstOrDefault(x => x.SubjectName == subjectName);
        }

        public IEnumerable<TeacherInfoDTO> GetPossibleTeachers(int id)
        {
            SubjectModel subjectFromDB = db.SubjectsRepository.GetByID(id);
            ICollection<TeacherInfoDTO> possibleTeachers = new List<TeacherInfoDTO>();
            foreach (TeacherToSubject tts in subjectFromDB.TeacherToSubjects)
            {
                possibleTeachers.Add(MakeTeacherInfoDTO(tts.Teacher));
            }

            return possibleTeachers;
        }

        public SubjectModel PostSubject(SubjectModel subject)
        {
            db.SubjectsRepository.Insert(subject);
            db.Save();

            logger.Info($"New subject with name {subject.SubjectName} was created. Subject fond for this subject is {subject.SubjectFond}");

            return subject;
        }

        public SubjectModel PutSubject(int id, SubjectModel subject)
        {
            SubjectModel subjectFromDB = db.SubjectsRepository.GetByID(id);
            subjectFromDB.SubjectName = subject.SubjectName;
            subjectFromDB.SubjectFond = subject.SubjectFond;
            subjectFromDB.Year = subject.Year;

            db.SubjectsRepository.Update(subjectFromDB);
            db.Save();

            logger.Info($"Information about subject with id {id} were changed. This subject now is called {subject.SubjectName}, has fond of {subject.SubjectFond}, and is tought on {subject.Year} year");

            return subjectFromDB;
        }

        public SubjectModel DeleteByID(int id)
        {
            SubjectModel subjectFromDB = db.SubjectsRepository.GetByID(id);
            db.SubjectsRepository.Delete(subjectFromDB);
            db.Save();

            logger.Warn($"Subject with id {id} was deleted ({subjectFromDB.SubjectName}).");

            return subjectFromDB;
        }

        private TeacherInfoDTO MakeTeacherInfoDTO(TeacherModel TeacherFromDB)
        {
            TeacherInfoDTO teacher = new TeacherInfoDTO(TeacherFromDB.Id, TeacherFromDB.FirstName, TeacherFromDB.LastName, TeacherFromDB.UserName, TeacherFromDB.Email, TeacherFromDB.SubjectFond, TeacherFromDB.PhoneNumber);

            foreach (TeacherToSubject tts in TeacherFromDB.TeacherToSubjects)
            {
                SubjectDisplayForTeacherDTO subjectForTeacher = new SubjectDisplayForTeacherDTO(tts.Subject.Id, tts.Subject.SubjectName);

                foreach (var students in tts.StudentToSubjects)
                {
                    StudentWithOneSubjectAndGradesDTO student = new StudentWithOneSubjectAndGradesDTO(students.Student.Id, students.Student.FirstName, students.Student.LastName);

                    foreach (var grade in students.Grades)
                    {
                        student.Grades.Add(grade);
                    }
                    subjectForTeacher.StudentsWithGrades.Add(student);
                }

                teacher.SubjectsWithStudents.Add(subjectForTeacher);
            };

            return teacher;
        }
    }
}