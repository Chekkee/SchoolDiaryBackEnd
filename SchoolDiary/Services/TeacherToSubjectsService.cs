using NLog;
using SchoolDiary.Models;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Services
{
    public class TeacherToSubjectsService : ITeacherToSubjectsService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public TeacherToSubjectsService(IUnitOfWork db)
        {
            this.db = db;
        }

        public TeacherModel PutSubjectToTeacher(int id, int subjectId)
        {
            TeacherModel tm = db.TeachersRepository.GetByID(id);
            SubjectModel sm = db.SubjectsRepository.GetByID(subjectId);
            TeacherToSubject teacherToSubject = new TeacherToSubject()
            {
                Teacher = tm,
                Subject = sm
            };

            logger.Info($"Teacher {tm.FirstName} {tm.LastName} now teaches subject {sm.SubjectName}");

            db.TeacherToSubjectsRepository.Insert(teacherToSubject);
            db.Save();

            return tm;
        }
    }
}