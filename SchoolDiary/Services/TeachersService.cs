using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;
using SchoolDiary.Models;
using SchoolDiary.Models.DTOs;
using SchoolDiary.Repositories;

namespace SchoolDiary.Services
{
    public class TeachersService : ITeachersService
    {
        private IUnitOfWork db;
        private StudentsService studentsService;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TeachersService(IUnitOfWork db, StudentsService studentsService)
        {
            this.db = db;
            this.studentsService = studentsService;
        }

        public bool ExistsID(string Id)
        {
            string id = Id.ToString();
            if(db.TeachersRepository.GetByID(id) == null)
            {
                return false;
            }
            return true;
        }

        public bool ExistsUsername(string username)
        {
            if (db.TeachersRepository.Get().FirstOrDefault(x=>x.UserName == username) == null)
            {
                return false;
            }
            return true;
        }

        public bool TeacherHasSubject(string id, int subjectId)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id);
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);

            if(TeacherFromDB.TeacherToSubjects.FirstOrDefault(x=>x.Subject == SubjectFromDB) == null)
            {
                return false;
            }
            return true;
        }

        public bool TeacherHasStudent(string id, string studentId, int subjectId)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id);
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(studentId);

            if(TeacherHasSubject(id, subjectId) == false || studentsService.StudentHasSubject(studentId, subjectId) == false)
            {
                return false;
            }

            StudentToSubject sts = db.StudentToSubjectsRepository.Get()
                .FirstOrDefault(x => x.Student.Id == StudentFromDB.Id && x.Subject.Id == SubjectFromDB.Id && x.TeacherToSubject.Teacher.Id == TeacherFromDB.Id);

            if(sts == null)
            {
                return false;
            }
            
            return true;
        }

        public IEnumerable<TeacherInfoDTO> Get()
        {
            IEnumerable<TeacherModel> AllTeachers = db.TeachersRepository.Get();
            ICollection<TeacherInfoDTO> AllTeacherDTOs = new List<TeacherInfoDTO>();
            foreach(TeacherModel teacherModel in AllTeachers)
            {
                AllTeacherDTOs.Add(MakeTeacherInfoDTO(teacherModel));
            }
            return AllTeacherDTOs;
        }

        public TeacherInfoDTO GetByID(string id)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id);
            TeacherInfoDTO teacher = MakeTeacherInfoDTO(TeacherFromDB);

            return teacher;
        }

        public TeacherInfoDTO GetByUsername(string username)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.Get().FirstOrDefault(x => x.UserName == username);
            return MakeTeacherInfoDTO(TeacherFromDB);
        }

        public ICollection<SubjectDisplayForTeacherDTO> GetRemainingSubjects(string id)
        {
            TeacherModel teacherFromDB = db.TeachersRepository.GetByID(id);
            IEnumerable<SubjectModel> remainingSubjects = db.SubjectsRepository.Get().Except(teacherFromDB.TeacherToSubjects.Select(x => x.Subject));

            ICollection<SubjectDisplayForTeacherDTO> remainingSubjectsDTO = new List<SubjectDisplayForTeacherDTO>();

            foreach(SubjectModel subject in remainingSubjects)
            {
                remainingSubjectsDTO.Add(new SubjectDisplayForTeacherDTO(subject.Id, subject.SubjectName));
            }
            return remainingSubjectsDTO;
        }

        public TeacherInfoDTO PutTeacher(string id, TeacherInfoDTO teacher)
        {
            TeacherModel teacherFromDB = db.TeachersRepository.GetByID(id);
            teacherFromDB.FirstName = teacher.FirstName;
            teacherFromDB.LastName = teacher.LastName;
            teacherFromDB.UserName = teacher.UserName;
            teacherFromDB.Email = teacher.Email;
            teacherFromDB.PhoneNumber = teacher.MobilePhone;

            db.TeachersRepository.Update(teacherFromDB);
            db.Save();

            logger.Info($"Information aboute teacher with id {id} were changed." +
                $"Changed teacher name: {teacher.FirstName}," +
                $"Changed teacher last name: {teacher.LastName}," +
                $"Changed teacher username: {teacher.UserName}," +
                $"Changed teacher email: {teacher.Email}," +
                $"changed teacher phone number: {teacher.MobilePhone}.");

            return MakeTeacherInfoDTO(teacherFromDB);
        }

        public TeacherInfoDTO PutSubjectToTeacher(string id, int subjectId)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id.ToString());
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);
            TeacherToSubject tts = new TeacherToSubject()
            {
                TeacherId = TeacherFromDB.Id,
                Teacher = TeacherFromDB,
                SubjectId = SubjectFromDB.Id,
                Subject = SubjectFromDB
            };
            TeacherFromDB.TeacherToSubjects.Add(tts);
            TeacherFromDB.SubjectFond += SubjectFromDB.SubjectFond;
            db.TeachersRepository.Update(TeacherFromDB);
            db.Save();

            logger.Info($"Subject with id {subjectId} ({SubjectFromDB.SubjectName}) was put to teacher with id {id} ({TeacherFromDB.FirstName} {TeacherFromDB.LastName})");

            return MakeTeacherInfoDTO(TeacherFromDB);
        }

        public TeacherInfoDTO PutStudentToTeacher(string id, string studentId, int subjectId)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id);
            StudentModel StudentFromDB = db.StudentsRepository.GetByID(studentId);
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);

            TeacherToSubject tts = db.TeacherToSubjectsRepository.Get().FirstOrDefault(x => x.Teacher == TeacherFromDB && x.Subject == SubjectFromDB);
            StudentToSubject sts = db.StudentToSubjectsRepository.Get().FirstOrDefault(x => x.Student == StudentFromDB && x.Subject == SubjectFromDB);

            sts.TeacherToSubject = tts;

            sts.TeacherToSubject.Teacher = tts.Teacher;
            sts.TeacherToSubject.TeacherId = tts.Teacher.Id;
            sts.TeacherToSubject.Subject = tts.Subject;
            sts.TeacherToSubject.SubjectId = tts.Subject.Id;

            db.StudentToSubjectsRepository.Update(sts);
            db.Save();

            logger.Info($"Student with id {id} ({StudentFromDB.FirstName} {StudentFromDB.LastName}) changed teacher on subject {SubjectFromDB.SubjectName}." +
                $"Teacher that now teaches that student has id: {id} ({TeacherFromDB.FirstName} {TeacherFromDB.LastName})");

            return MakeTeacherInfoDTO(TeacherFromDB);
        }

        public TeacherModel DeleteTeacher(string Id)
        {
            string id = Id.ToString();
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id);
            db.TeachersRepository.Delete(TeacherFromDB);
            db.Save();

            logger.Warn($"Teacher with id {Id} ({TeacherFromDB.FirstName} {TeacherFromDB.LastName}) was deleted");

            return TeacherFromDB;
        }
        
        public TeacherInfoDTO DeleteSubjectFromTeacher(string id, int subjectId)
        {
            TeacherModel TeacherFromDB = db.TeachersRepository.GetByID(id.ToString());
            SubjectModel SubjectFromDB = db.SubjectsRepository.GetByID(subjectId);
            TeacherToSubject tts = db.TeacherToSubjectsRepository.Get().FirstOrDefault(x => x.Teacher == TeacherFromDB && x.Subject == SubjectFromDB);
            TeacherFromDB.TeacherToSubjects.Remove(tts);
            TeacherFromDB.SubjectFond -= SubjectFromDB.SubjectFond;
            db.TeachersRepository.Update(TeacherFromDB);
            db.Save();

            logger.Info($"Teacher with id {id} ({TeacherFromDB.FirstName} {TeacherFromDB.LastName}) no longer teaches subject with id {subjectId} ({SubjectFromDB.SubjectName})");

            return MakeTeacherInfoDTO(TeacherFromDB);
        }

        public StudentModel PostGradeToStudent(string id, string studentId, int subjectId, Grade grade)
        {
            TeacherModel teacher = db.TeachersRepository.GetByID(id);
            StudentModel student = db.StudentsRepository.GetByID(studentId);
            SubjectModel subject = db.SubjectsRepository.GetByID(subjectId);

            StudentToSubject sts = db.StudentToSubjectsRepository.Get().FirstOrDefault(x => x.Student == student && x.Subject == subject && x.TeacherToSubject.Teacher == teacher);
            sts.Grades.Add(grade);

            db.StudentToSubjectsRepository.Update(sts);
            db.Save();

            logger.Info($"Student with id {studentId} got new grade ({grade.GradeValue}) from subject {subject.SubjectName}. Teacher {teacher.FirstName} {teacher.LastName} gave that grade");

            return student;
        }

        private TeacherInfoDTO MakeTeacherInfoDTO(TeacherModel TeacherFromDB)
        {
            TeacherInfoDTO teacher = new TeacherInfoDTO(TeacherFromDB.Id, TeacherFromDB.FirstName, TeacherFromDB.LastName, TeacherFromDB.UserName, TeacherFromDB.Email, TeacherFromDB.SubjectFond, TeacherFromDB.PhoneNumber);

            foreach (TeacherToSubject tts in TeacherFromDB.TeacherToSubjects)
            {
                SubjectDisplayForTeacherDTO subjectForTeacher = new SubjectDisplayForTeacherDTO(tts.Subject.Id ,tts.Subject.SubjectName);

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