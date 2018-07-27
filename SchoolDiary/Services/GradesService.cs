using NLog;
using SchoolDiary.Models;
using SchoolDiary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolDiary.Services
{
    public class GradesService : IGradesService
    {
        private IUnitOfWork db;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public GradesService(IUnitOfWork db)
        {
            this.db = db;
        }

        public bool ExistsID(int id)
        {
            if(db.GradesRepository.GetByID(id) == null)
            {
                return false;
            }
            return true;
        }

        public Grade GetByID(int id)
        {
            return db.GradesRepository.GetByID(id);
        }

        public Grade PutGrade(int id, Grade grade)
        {
            Grade gradeFromDB = db.GradesRepository.GetByID(id);
            gradeFromDB.GradeDate = grade.GradeDate;
            gradeFromDB.GradeValue = grade.GradeValue;

            db.GradesRepository.Update(gradeFromDB);
            db.Save();
            logger.Info($"Grade with id of {gradeFromDB.Id} was changed, its value now is {grade.GradeValue}");

            return gradeFromDB;
        }

        public Grade DeleteByID(int id)
        {
            Grade GradeFromDB = db.GradesRepository.GetByID(id);
            db.GradesRepository.Delete(GradeFromDB);
            db.Save();

            logger.Warn($"Deleting of grade occured. Grade id: {GradeFromDB.Id}");

            return GradeFromDB;
        }
    }
}